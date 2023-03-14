using AutoMapper;
using HotelManagement.Business.Repositories.Interfaces;
using HotelManagement.DataAccess.Data;
using HotelManagement.DataAccess.Models;
using HotelManagement.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Business.Repositories.Implementations;

public class HotelImageRepository : IHotelImagesRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public HotelImageRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<int> CreateHotelRoomImage(HotelRoomImageDto imageDto)
    {
        try
        {
            var hotelRoomImage = _mapper.Map<HotelRoomImageDto, HotelRoomImage>(imageDto);

            await _db.HotelRoomsImage.AddAsync(hotelRoomImage);
            return await _db.SaveChangesAsync();
        }
        catch
        {
            return 0;
        }
    }

    public async Task<int> DeleteHotelRoomImageByImageId(int imageId)
    {
        try
        {
            var image = await _db.HotelRoomsImage.FindAsync(imageId);

            if (image == null)
                return 0;
            
            _db.HotelRoomsImage.Remove(image);
            return await _db.SaveChangesAsync();
        }
        catch
        {
            return 0;
        }
    }

    public async Task<int> DeleteHotelRoomImagesByRoomId(int roomId)
    {
        try
        {
            var images = await _db.HotelRoomsImage.Where(a => a.RoomId == roomId).ToListAsync();
            _db.HotelRoomsImage.RemoveRange(images);
            return await _db.SaveChangesAsync();
        }
        catch
        {
            return 0;
        }
    }

    public async Task<IEnumerable<HotelRoomImageDto>> GetHotelRoomImages(int roomId)
    {
        try
        {
            var hotelRoomImages = await _db.HotelRoomsImage.Where(a => a.RoomId == roomId).ToListAsync();

            return _mapper.Map<List<HotelRoomImage>, List<HotelRoomImageDto>>(hotelRoomImages);
        }
        catch
        {
            return new List<HotelRoomImageDto>();
        }
    }
}