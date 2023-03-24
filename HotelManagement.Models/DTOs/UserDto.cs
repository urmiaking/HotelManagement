namespace HotelManagement.Models.DTOs;

public class UserDto
{
    public string Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public string? PhoneNo { get; set; }
}