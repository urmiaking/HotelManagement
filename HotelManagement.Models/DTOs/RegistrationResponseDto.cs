namespace HotelManagement.Models.DTOs;

public class RegistrationResponseDto
{
    public RegistrationResponseDto()
    {
        Errors = new Dictionary<string, string[]>();
    }

    public bool IsRegistrationSuccessful { get; set; }
    public IDictionary<string, string[]> Errors { get; set; }
}