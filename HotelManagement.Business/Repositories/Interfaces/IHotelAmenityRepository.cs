using HotelManagement.Models.DTOs;

namespace HotelManagement.Business.Repositories.Interfaces;

public interface IHotelAmenityRepository
{
    Task<HotelAmenityDto> CreateHotelAmenity(HotelAmenityDto hotelAmenityDto);
    Task<HotelAmenityDto> UpdateHotelAmenity(int amenityId, HotelAmenityDto hotelAmenityDto);
    Task<HotelAmenityDto> GetHotelAmenity(int amenityId);
    Task<int> DeleteHotelAmenity(int amenityId);
    Task<IEnumerable<HotelAmenityDto>> GetAllHotelAmenities();
    Task<HotelAmenityDto> IsAmenityUnique(string name, int amenityId);
}