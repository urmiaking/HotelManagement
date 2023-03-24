using System.Security.Claims;
using System.Text;
using HotelManagement.Api.Settings;
using HotelManagement.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HotelManagement.Api.Helpers;

public class AccountHelpers
{
    private readonly ApiSettings _apiSettings;
    private readonly UserManager<ApplicationUser> _userManager;

    public AccountHelpers(IOptions<ApiSettings> options, UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
        _apiSettings = options.Value;
    }

    public SigningCredentials GetSigningCredentials()
    {
        var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_apiSettings.SecretKey));

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    public async Task<List<Claim>> GetClaims(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Email),
            new(ClaimTypes.Email, user.Email),
            new("Id", user.Id)
        };

        var roles = await _userManager.GetRolesAsync(user);

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return claims;
    }
}