using AutoMapper;
using HotelManagement.DataAccess.Models;
using HotelManagement.Models.DTOs;

namespace HotelManagement.Business.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<HotelRoomDto, HotelRoom>();
        CreateMap<HotelRoom, HotelRoomDto>();
    }
}