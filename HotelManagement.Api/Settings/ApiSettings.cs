namespace HotelManagement.Api.Settings;

public class ApiSettings
{
    public required string SecretKey { get; set; }
    public required string ValidAudience { get; set; }
    public required string ValidIssuer { get; set; }
}