using System.Globalization;
using HotelManagement.Business.Repositories.Interfaces;
using HotelManagement.Common;
using HotelManagement.DataAccess.Models;
using HotelManagement.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HotelManagement.Api.Endpoints;

public static class HotelRoomEndpoints
{
    public static void MapHotelRoomEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api").WithTags(nameof(HotelRoom));

        group.MapGet("/hotelrooms", GetAllHotelRooms).WithName("GetHotelRooms").WithOpenApi();

        group.MapGet("/hotelroom", GetHotelRoom).WithName("GetHotelRoom").WithOpenApi();
    }

    private static async Task<IResult> GetHotelRoom(int roomId, string? checkInDate, string? checkOutDate, IHotelRoomRepository repository)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrEmpty(checkInDate) || string.IsNullOrEmpty(checkOutDate))
            errors.Add("CheckInError", new[] { "Fill in the parameters" });


        if (!DateTime.TryParseExact(checkInDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            errors.Add("CheckInError", new[] { "Invalid CheckInDate format" });


        if (!DateTime.TryParseExact(checkOutDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            errors.Add("CheckOutError", new[] { "Invalid CheckOutDate format" });


        if (errors.Any())
            return TypedResults.BadRequest(new ResponseDto
            {
                Succeed = false,
                Errors = errors
            });

        var result = await repository.GetHotelRoom(roomId, checkInDate, checkOutDate);
        return TypedResults.Ok(result);
    }

    //[Authorize(Roles = StaticVariables.RoleAdmin)]
    private static async Task<IResult> GetAllHotelRooms(string? checkInDate, string? checkOutDate, IHotelRoomRepository repository)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrEmpty(checkInDate) || string.IsNullOrEmpty(checkOutDate))
            errors.Add("CheckInError", new[] { "Fill in the parameters" });
        

        if (!DateTime.TryParseExact(checkInDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            errors.Add("CheckInError", new[] { "Invalid CheckInDate format" });
        

        if (!DateTime.TryParseExact(checkOutDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            errors.Add("CheckOutError", new[] { "Invalid CheckOutDate format" });
        

        if (errors.Any())
            return TypedResults.BadRequest(new ResponseDto
            {
                Succeed = false,
                Errors = errors
            });

        var result = await repository.GetAllHotelRooms(checkInDate, checkOutDate);
        return TypedResults.Ok(result);
    }
}