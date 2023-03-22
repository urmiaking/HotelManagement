using HotelManagement.DataAccess.Data;

namespace HotelManagement.Server.Services;

public static class WebApplicationExtensions
{
    public static void UseInitializer(this WebApplication application)
    {
        using var scope = application.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var dbInitializer = scope.ServiceProvider.GetService<IDbInitializer>(); //Service locator

        dbInitializer!.Initialize();
    }
}