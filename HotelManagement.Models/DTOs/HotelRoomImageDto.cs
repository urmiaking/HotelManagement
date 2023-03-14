namespace HotelManagement.Models.DTOs;

public class HotelRoomImageDto
{
    public int Id { get; set; }
    public string RoomImageUrl { get; set; } = string.Empty;
    public int RoomId { get; set; }
}