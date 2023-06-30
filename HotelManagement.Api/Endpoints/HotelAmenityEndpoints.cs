using HotelManagement.Business.Repositories.Interfaces;
using HotelManagement.DataAccess.Models;

namespace HotelManagement.Api.Endpoints;

public static class HotelAmenityEndpoints
{
    public static void MapHotelAmenityEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api").WithTags(nameof(HotelAmenity));

        group.MapGet("/hotelamenities", GetAllHotelAmenities).WithName("GetHotelAmenities").WithOpenApi();
    }

    private static async Task<IResult> GetAllHotelAmenities(IHotelAmenityRepository repository)
    {
        var result = await repository.GetAllHotelAmenities();
        return TypedResults.Ok(result);
    }
}