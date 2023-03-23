using HotelManagement.Business.Repositories.Interfaces;
using HotelManagement.DataAccess.Models;
using HotelManagement.Models.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HotelManagement.Api.Endpoints;

public static class HotelRoomEndpoints
{
    public static void MapHotelRoomEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api").WithTags(nameof(HotelRoom));

        group.MapGet("/hotelrooms", async (IHotelRoomRepository repository) => await repository.GetAllHotelRooms())
            .WithName("GetHotelRooms")
            .WithOpenApi();

        group.MapGet("/hotelrooms/{id}", async Task<Results<Ok<HotelRoomDto>, NotFound>> (int id, IHotelRoomRepository repository)
                => await repository.GetHotelRoom(id) is HotelRoomDto model
                ? TypedResults.Ok(model) : TypedResults.NotFound())
            .WithName("GetHotelRoomById")
            .WithOpenApi();
    }
}