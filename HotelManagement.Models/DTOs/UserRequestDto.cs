using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models.DTOs;

public class UserRequestDto
{
    public required string Name { get; set; }

    public required string Email { get; set; }
    
    public string? PhoneNo { get; set; }
    
    public required string Password { get; set; }
    
    public required string ConfirmPassword { get; set; }
}