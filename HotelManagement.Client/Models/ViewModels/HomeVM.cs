namespace HotelManagement.Client.Models.ViewModels;

public class HomeVm
{
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; }
    public int NumberOfNights { get; set; } = 1;
}