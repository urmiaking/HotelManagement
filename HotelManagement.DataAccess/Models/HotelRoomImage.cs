using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.DataAccess.Models;

public class HotelRoomImage
{
    public int Id { get; set; }
    public string RoomImageUrl { get; set; } = string.Empty;
    public int RoomId { get; set; }

    [ForeignKey(nameof(RoomId))] 
    public virtual HotelRoom HotelRoom { get; set; } = default!;
}