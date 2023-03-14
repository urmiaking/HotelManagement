using HotelManagement.Models.DTOs;

namespace HotelManagement.Business.Repositories.Interfaces;

public interface IHotelImagesRepository
{
    Task<int> CreateHotelRoomImage(HotelRoomImageDto imageDto);
    Task<int> DeleteHotelRoomImageByImageId(int imageId);
    Task<int> DeleteHotelRoomImagesByRoomId(int roomId);
    Task<IEnumerable<HotelRoomImageDto>> GetHotelRoomImages(int roomId);
}