﻿using HotelManagement.Models.DTOs;

namespace HotelManagement.Client.Services.Abstracts;

public interface IHotelRoomService
{
    public Task<IEnumerable<HotelRoomDto>?> GetHotelRooms(string checkInDate, string checkOutDate);
    public Task<HotelRoomDto?> GetHotelRoomDetails(int roomId, string checkInDate, string checkOutDate);
}