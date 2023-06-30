using System.Net.Http.Json;
using HotelManagement.Client.Services.Abstracts;
using HotelManagement.Models.DTOs;

namespace HotelManagement.Client.Services.Implementations;

public class HotelAmenityService : IHotelAmenityService
{
    private readonly HttpClient _httpClient;

    public HotelAmenityService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<HotelAmenityDto>?> GetAllHotelAmenities()
    {
        try
        {
            var response = await _httpClient.GetAsync("/api/hotelamenities");

            var hotelAmenities = await response.Content.ReadFromJsonAsync<IEnumerable<HotelAmenityDto>>();

            return hotelAmenities;
        }
        catch
        {
            return null;
        }
        
    }
}