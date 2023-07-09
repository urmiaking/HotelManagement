using HotelManagement.Models.DTOs;

namespace HotelManagement.Client.Services.Abstracts;

public interface IRoomOrderDetailsService
{
    Task<RoomOrderDetailsDto> SaveRoomOrderDetails(RoomOrderDetailsDto roomOrderDetailsDto);
    Task<RoomOrderDetailsDto> MarkPaymentSuccessful(RoomOrderDetailsDto roomOrderDetailsDto);
}