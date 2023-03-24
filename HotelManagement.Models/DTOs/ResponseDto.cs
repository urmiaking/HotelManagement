namespace HotelManagement.Models.DTOs;

public class ResponseDto
{
    public ResponseDto()
    {
        Errors = new Dictionary<string, string[]>();
    }

    public bool Succeed { get; set; }
    public IDictionary<string, string[]> Errors { get; set; }
}