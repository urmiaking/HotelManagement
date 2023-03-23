using FluentValidation;
using HotelManagement.Business.Repositories.Interfaces;
using HotelManagement.Common;
using HotelManagement.DataAccess.Models;
using HotelManagement.Models.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HotelManagement.Api.Endpoints;

public static class AccountEndpoints
{
    public static void MapAccountEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/account").WithTags(nameof(ApplicationUser)).RequireAuthorization();

        group.MapPost("/signup", async Task<Results<Created<string>, BadRequest<RegistrationResponseDto>, ValidationProblem>>
                ([FromBody] UserRequestDto userRequest,
                    IValidator<UserRequestDto> validator,
                    UserManager<ApplicationUser> userManager) =>
            {
                var validationResult = await validator.ValidateAsync(userRequest);

                if (!validationResult.IsValid)
                {
                    return TypedResults.BadRequest(new RegistrationResponseDto
                    {
                        IsRegistrationSuccessful = false,
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
                    return TypedResults.BadRequest(new RegistrationResponseDto
                    {
                        IsRegistrationSuccessful = false,
                        Errors = result.Errors.ToDictionary(a => a.Code, _ => result.Errors.Select(a => a.Description).ToArray())
                    });
                }

                var roleResult = await userManager.AddToRoleAsync(user, StaticVariables.RoleCustomer);

                if (!roleResult.Succeeded)
                {
                    await userManager.DeleteAsync(user);

                    return TypedResults.BadRequest(new RegistrationResponseDto
                    {
                        IsRegistrationSuccessful = false,
                        Errors = roleResult.Errors.ToDictionary(a => a.Code, _ => result.Errors.Select(a => a.Description).ToArray())
                    });
                }

                var userId = (await userManager.FindByEmailAsync(user.Email))!.Id;

                return TypedResults.Created("/api/account/profile/{id}", userId);
            })
            .WithName("SignUp")
            .WithOpenApi()
            .AllowAnonymous();
    }
}