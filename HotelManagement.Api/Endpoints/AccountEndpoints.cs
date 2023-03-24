using System.IdentityModel.Tokens.Jwt;
using FluentValidation;
using HotelManagement.Common;
using HotelManagement.DataAccess.Models;
using HotelManagement.Models.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HotelManagement.Api.Helpers;
using HotelManagement.Api.Settings;
using Microsoft.Extensions.Options;

namespace HotelManagement.Api.Endpoints;

public static class AccountEndpoints
{
    public static void MapAccountEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/account").WithTags(nameof(ApplicationUser));

        group.MapPost("/signup", async Task<Results<Created<string>, BadRequest<ResponseDto>>>
                ([FromBody] UserRequestDto userRequest, 
                    IValidator<UserRequestDto> validator,
                    UserManager<ApplicationUser> userManager) =>
            {
                var validationResult = await validator.ValidateAsync(userRequest);

                if (!validationResult.IsValid)
                {
                    return TypedResults.BadRequest(new ResponseDto
                    {
                        Succeed = false,
                        Errors = validationResult.ToDictionary()
                    });
                }

                var user = new ApplicationUser
                {
                    UserName = userRequest.Email,
                    Email = userRequest.Email,
                    Name = userRequest.Name,
                    PhoneNumber = userRequest.PhoneNo,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, userRequest.Password);

                if (!result.Succeeded)
                {
                    return TypedResults.BadRequest(new ResponseDto
                    {
                        Succeed = false,
                        Errors = result.Errors.ToDictionary(a => a.Code, _ => result.Errors.Select(a => a.Description).ToArray())
                    });
                }

                var roleResult = await userManager.AddToRoleAsync(user, StaticVariables.RoleCustomer);

                if (!roleResult.Succeeded)
                {
                    await userManager.DeleteAsync(user);

                    return TypedResults.BadRequest(new ResponseDto
                    {
                        Succeed = false,
                        Errors = roleResult.Errors.ToDictionary(a => a.Code, _ => result.Errors.Select(a => a.Description).ToArray())
                    });
                }

                var userId = (await userManager.FindByEmailAsync(user.Email))!.Id;

                return TypedResults.Created("/api/account/profile/{id}", userId);
            })
            .WithName("SignUp")
            .WithOpenApi()
            .AllowAnonymous();

        group.MapPost("/signin", async Task<Results<Ok<AuthenticationResponseDto>, BadRequest<ResponseDto>, UnauthorizedHttpResult>>
                ([FromBody] AuthenticationDto authenticationDto, 
                    IValidator<AuthenticationDto> validator, 
                    SignInManager<ApplicationUser> signInManager, 
                    UserManager<ApplicationUser> userManager, 
                    IOptions<ApiSettings> options) =>
            {
                var validationResult = await validator.ValidateAsync(authenticationDto);

                if (!validationResult.IsValid)
                {
                    return TypedResults.BadRequest(new ResponseDto
                    {
                        Succeed = false,
                        Errors = validationResult.ToDictionary()
                    });
                }

                var apiSettings = options.Value;

                var result = await signInManager.PasswordSignInAsync(authenticationDto.UserName,
                    authenticationDto.Password, false, false);

                if (!result.Succeeded)
                    return TypedResults.Unauthorized();

                var user = await userManager.FindByNameAsync(authenticationDto.UserName);

                if (user == null)
                    return TypedResults.Unauthorized();


                var accountHelper = new AccountHelpers(options, userManager);

                var signingCredentials = accountHelper.GetSigningCredentials();

                var claims = await accountHelper.GetClaims(user);

                var tokenOptions = new JwtSecurityToken(
                    issuer: apiSettings.ValidIssuer,
                    audience: apiSettings.ValidAudience,
                    claims: claims,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: signingCredentials);

                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return TypedResults.Ok(new AuthenticationResponseDto
                {
                    Succeed = true,
                    Token = token,
                    UserDto = new UserDto
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Name = user.Name,
                        PhoneNo = user.PhoneNumber
                    }
                });
            })
            .WithName("SignIn")
            .WithOpenApi()
            .AllowAnonymous();
    }
}