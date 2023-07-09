using HotelManagement.Client.Services.Abstracts;
using HotelManagement.Models.DTOs;

namespace HotelManagement.Client.Services.Implementations;

public class RoomOrderDetailsService : IRoomOrderDetailsService
{
    private readonly HttpClient _httpClient;

    public RoomOrderDetailsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<RoomOrderDetailsDto> SaveRoomOrderDetails(RoomOrderDetailsDto roomOrderDetailsDto)
    {
        throw new NotImplementedException();
    }

    public Task<RoomOrderDetailsDto> MarkPaymentSuccessful(RoomOrderDetailsDto roomOrderDetailsDto)
    {
        throw new NotImplementedException();
    }
}