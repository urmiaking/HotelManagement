using Microsoft.AspNetCore.Identity;

namespace HotelManagement.DataAccess.Models;

public class ApplicationUser : IdentityUser
{
    public string? Name { get; set; }
}