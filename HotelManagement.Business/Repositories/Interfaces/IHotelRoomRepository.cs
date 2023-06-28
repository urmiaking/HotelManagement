using HotelManagement.Models.DTOs;

namespace HotelManagement.Business.Repositories.Interfaces;

public interface IHotelRoomRepository
{
    Task<HotelRoomDto> CreateHotelRoom(HotelRoomDto hotelRoomDto);
    Task<HotelRoomDto> UpdateHotelRoom(int roomId, HotelRoomDto hotelRoomDto);
    Task<HotelRoomDto> GetHotelRoom(int roomId, string? checkInDate = null, string? checkOutDate = null);
    Task<int> DeleteHotelRoom(int roomId);
    Task<IEnumerable<HotelRoomDto>> GetAllHotelRooms(string? checkInDate = null, string? checkOutDate = null);
    Task<HotelRoomDto> IsRoomUnique(string name, int roomId);
}