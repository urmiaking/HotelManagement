using AutoMapper;
using HotelManagement.Business.Repositories.Interfaces;
using HotelManagement.DataAccess.Data;
using HotelManagement.DataAccess.Models;
using HotelManagement.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Business.Repositories.Implementations;

public class HotelRoomRepository : IHotelRoomRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public HotelRoomRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<HotelRoomDto> CreateHotelRoom(HotelRoomDto hotelRoomDto)
    {
        var hotelRoom = _mapper.Map<HotelRoomDto, HotelRoom>(hotelRoomDto);
        hotelRoom.CreateDate = DateTime.Now;

        var addedHotelRoom = await _db.HotelRooms.AddAsync(hotelRoom);
        await _db.SaveChangesAsync();

        return _mapper.Map<HotelRoom, HotelRoomDto>(addedHotelRoom.Entity);
    }

    public async Task<HotelRoomDto> UpdateHotelRoom(int roomId, HotelRoomDto hotelRoomDto)
    {
        try
        {
            if (roomId != hotelRoomDto.Id)
            {
                return null;
            }

            var roomDetails = await _db.HotelRooms.FindAsync(roomId);

            if (roomDetails != null)
            {
                var room = _mapper.Map(hotelRoomDto, roomDetails);
                room.UpdateDate = DateTime.Now;

                var updatedRoom = _db.HotelRooms.Update(room);

                await _db.SaveChangesAsync();

                return _mapper.Map<HotelRoom, HotelRoomDto>(updatedRoom.Entity);
            }

            return null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<HotelRoomDto> GetHotelRoom(int roomId, string? checkInDate = null, string? checkOutDate = null)
    {
        try
        {
            var hotelRoom = await _db.HotelRooms
                .Include(a => a.Images)
                .FirstOrDefaultAsync(a => a.Id == roomId);

            return _mapper.Map<HotelRoom, HotelRoomDto>(hotelRoom!);
        }
        catch
        {
            return null;
        }
    }

    public async Task<int> DeleteHotelRoom(int roomId)
    {
        var room = await _db.HotelRooms.FindAsync(roomId);

        if (room is not null)
        {
            var roomImages = await _db.HotelRoomsImage.Where(a => a.RoomId == roomId).ToListAsync();

            _db.HotelRoomsImage.RemoveRange(roomImages);

            _db.HotelRooms.Remove(room);
            return await _db.SaveChangesAsync();
        }

        return 0;
    }

    public async Task<IEnumerable<HotelRoomDto>> GetAllHotelRooms(string? checkInDate, string? checkOutDate)
    {
        try
        {
            return _mapper.Map<IEnumerable<HotelRoom>, IEnumerable<HotelRoomDto>>(
                await _db.HotelRooms.Include(a => a.Images).ToListAsync());
        }
        catch
        {
            return null;
        }
    }

    public async Task<HotelRoomDto> IsRoomUnique(string name, int roomId = 0)
    {
        try
        {
            if (roomId == 0)
            {
                var hotelRoom = await _db.HotelRooms.FirstOrDefaultAsync(a =>
                    a.Name == name);

                return _mapper.Map<HotelRoom, HotelRoomDto>(hotelRoom!);
            }
            else
            {
                var hotelRoom = await _db.HotelRooms.FirstOrDefaultAsync(a =>
                    a.Name == name && a.Id != roomId);

                return _mapper.Map<HotelRoom, HotelRoomDto>(hotelRoom!);
            }
        }
        catch
        {
            return null;
        }
    }
}