using HotelManagement.Models.DTOs;

namespace HotelManagement.Business.Repositories.Interfaces;

public interface IRoomOrderDetailsRepository
{
    Task<RoomOrderDetailsDto?> Create(RoomOrderDetailsDto roomOrderDetailsDto);
    Task<RoomOrderDetailsDto> MarkPaymentSuccessful(int id);
    Task<RoomOrderDetailsDto?> GetRoomOrderDetail(int roomOrderId);
    Task<IEnumerable<RoomOrderDetailsDto>> GetAllRoomOrderDetails();

    Task<bool> UpdateOrderStatus(int roomId, string status);
    Task<bool> IsRoomBooked(int roomId, DateTime checkInDate, DateTime checkOutDate);
}