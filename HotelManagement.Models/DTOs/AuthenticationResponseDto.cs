namespace HotelManagement.Models.DTOs;

public class AuthenticationResponseDto : ResponseDto
{
    public string? Token { get; set; }
    public UserDto? UserDto { get; set; }
}