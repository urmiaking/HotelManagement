using HotelManagement.Common;
using HotelManagement.DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Server.Services;

public class DbInitializer : IDbInitializer
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DbInitializer(ApplicationDbContext context, RoleManager<IdentityRole> roleManager,
        UserManager<IdentityUser> userManager)
    {
        _context = context;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public void Initialize()
    {
        try
        {
            if (_context.Database.GetPendingMigrations().Any())
            {
                _context.Database.Migrate();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        if (_context.Roles.Any(x => x.Name == StaticVariables.RoleAdmin))
            return;

        _roleManager.CreateAsync(new IdentityRole(StaticVariables.RoleAdmin)).GetAwaiter().GetResult();
        _roleManager.CreateAsync(new IdentityRole(StaticVariables.RoleCustomer)).GetAwaiter().GetResult();
        _roleManager.CreateAsync(new IdentityRole(StaticVariables.RoleEmployee)).GetAwaiter().GetResult();

        _userManager.CreateAsync(new IdentityUser
        {
            UserName = "admin@admin.com",
            Email = "admin@admin.com",
            EmailConfirmed = true
        }, "Admin123*").GetAwaiter().GetResult();

        var user = _context.Users.FirstOrDefault(a => a.Email == "admin@admin.com");

        _userManager.AddToRoleAsync(user!, StaticVariables.RoleAdmin).GetAwaiter().GetResult();
    }
}