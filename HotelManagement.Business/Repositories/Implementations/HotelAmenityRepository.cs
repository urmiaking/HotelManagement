using AutoMapper;
using HotelManagement.Business.Repositories.Interfaces;
using HotelManagement.DataAccess.Data;
using HotelManagement.DataAccess.Models;
using HotelManagement.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Business.Repositories.Implementations;

public class HotelAmenityRepository : IHotelAmenityRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public HotelAmenityRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<HotelAmenityDto> CreateHotelAmenity(HotelAmenityDto hotelAmenityDto)
    {
        var hotelAmenity = _mapper.Map<HotelAmenityDto, HotelAmenity>(hotelAmenityDto);
        var addedHotelAmenity = await _db.HotelAmenities.AddAsync(hotelAmenity);
        await _db.SaveChangesAsync();

        return _mapper.Map<HotelAmenity, HotelAmenityDto>(addedHotelAmenity.Entity);
    }

    public async Task<HotelAmenityDto> UpdateHotelAmenity(int amenityId, HotelAmenityDto hotelAmenityDto)
    {
        try
        {
            if (amenityId != hotelAmenityDto.Id)
            {
                return null;
            }

            var amenityDetails = await _db.HotelAmenities.FindAsync(amenityId);

            if (amenityDetails != null)
            {
                var amenity = _mapper.Map(hotelAmenityDto, amenityDetails);
                
                var updatedAmenity = _db.HotelAmenities.Update(amenity);

                await _db.SaveChangesAsync();

                return _mapper.Map<HotelAmenity, HotelAmenityDto>(updatedAmenity.Entity);
            }

            return null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<HotelAmenityDto> GetHotelAmenity(int amenityId)
    {
        try
        {
            var hotelAmenity = await _db.HotelAmenities
                .FirstOrDefaultAsync(a => a.Id == amenityId);

            return _mapper.Map<HotelAmenity, HotelAmenityDto>(hotelAmenity!);
        }
        catch
        {
            return null;
        }
    }

    public async Task<int> DeleteHotelAmenity(int amenityId)
    {
        try
        {
            var amenity = await _db.HotelAmenities.FindAsync(amenityId);

            if (amenity != null)
            {
                _db.HotelAmenities.Remove(amenity);
                return await _db.SaveChangesAsync();
            }

            return 0;
        }
        catch
        {
            return 0;
        }
    }

    public async Task<IEnumerable<HotelAmenityDto>> GetAllHotelAmenities()
    {
        try
        {
            return _mapper.Map<IEnumerable<HotelAmenity>, IEnumerable<HotelAmenityDto>>(
                await _db.HotelAmenities.ToListAsync());
        }
        catch
        {
            return null;
        }
    }

    public async Task<HotelAmenityDto> IsAmenityUnique(string name, int amenityId)
    {
        try
        {
            if (amenityId == 0)
            {
                var hotelAmenity = await _db.HotelAmenities.FirstOrDefaultAsync(a => a.Name == name);

                return _mapper.Map<HotelAmenity, HotelAmenityDto>(hotelAmenity!);
            }
            else
            {
                var hotelAmenity = await _db.HotelAmenities.FirstOrDefaultAsync(a =>
                    a.Name == name && a.Id != amenityId);

                return _mapper.Map<HotelAmenity, HotelAmenityDto>(hotelAmenity!);
            }
        }
        catch
        {
            return null;
        }
    }
}