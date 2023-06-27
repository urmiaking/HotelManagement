using System.Net.Http.Json;
using HotelManagement.Client.Services.Abstracts;
using HotelManagement.Models.DTOs;

namespace HotelManagement.Client.Services.Implementations;

public class HotelRoomService : IHotelRoomService
{
    private readonly HttpClient _httpClient;

    public HotelRoomService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<HotelRoomDto>?> GetHotelRooms(string checkInDate, string checkOutDate)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/api/hotelrooms?checkInDate={checkInDate}&checkOutDate={checkOutDate}");
            return await response.Content.ReadFromJsonAsync<IEnumerable<HotelRoomDto>>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<HotelRoomDto?> GetHotelRoomDetails(int roomId, string checkInDate, string checkOutDate)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/api/hotelrooms?roomId={roomId}&checkInDate={checkInDate}&checkOutDate={checkOutDate}");
            return await response.Content.ReadFromJsonAsync<HotelRoomDto>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}