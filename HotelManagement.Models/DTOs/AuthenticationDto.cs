namespace HotelManagement.Models.DTOs;

public class AuthenticationDto
{
    public required string UserName { get; set; }

    public required string Password { get; set; }

}