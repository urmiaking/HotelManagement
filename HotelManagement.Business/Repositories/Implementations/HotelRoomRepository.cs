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
                var room = _mapper.Map<HotelRoomDto, HotelRoom>(hotelRoomDto, roomDetails);
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

    public async Task<HotelRoomDto> GetHotelRoom(int roomId)
    {
        try
        {
            var hotelRoom = await _db.HotelRooms.FindAsync(roomId);

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
            _db.HotelRooms.Remove(room);
            return await _db.SaveChangesAsync();
        }

        return 0;
    }

    public async Task<IEnumerable<HotelRoomDto>> GetAllHotelRooms()
    {
        try
        {
            return _mapper.Map<IEnumerable<HotelRoom>, IEnumerable<HotelRoomDto>>(await _db.HotelRooms.ToListAsync());
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
                    string.Equals(a.Name, name, StringComparison.CurrentCultureIgnoreCase));

                return _mapper.Map<HotelRoom, HotelRoomDto>(hotelRoom!);
            }
            else
            {
                var hotelRoom = await _db.HotelRooms.FirstOrDefaultAsync(a =>
                    string.Equals(a.Name, name, StringComparison.CurrentCultureIgnoreCase) && a.Id != roomId);

                return _mapper.Map<HotelRoom, HotelRoomDto>(hotelRoom!);
            }
        }
        catch
        {
            return null;
        }
    }
}