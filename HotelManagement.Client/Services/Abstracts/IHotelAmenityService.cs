using HotelManagement.Models.DTOs;

namespace HotelManagement.Client.Services.Abstracts;

public interface IHotelAmenityService
{
    Task<IEnumerable<HotelAmenityDto>?> GetAllHotelAmenities();
}