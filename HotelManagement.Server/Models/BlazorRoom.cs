namespace HotelManagement.Server.Models;

public class BlazorRoom
{
    public int Id { get; set; }
    public required string RoomName { get; set; }
    public double Price { get; set; }
    public bool IsActive { get; set; }
    public List<BlazorRoomProp>? RoomProps { get; set; }
}