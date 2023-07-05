using AutoMapper;
using HotelManagement.DataAccess.Models;
using HotelManagement.Models.DTOs;

namespace HotelManagement.Business.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<HotelRoomDto, HotelRoom>().ReverseMap();
        CreateMap<HotelRoomImage, HotelRoomImageDto>().ReverseMap();
        CreateMap<HotelAmenity, HotelAmenityDto>().ReverseMap();
        CreateMap<RoomOrderDetail, RoomOrderDetailsDto>().ReverseMap();
    }
}