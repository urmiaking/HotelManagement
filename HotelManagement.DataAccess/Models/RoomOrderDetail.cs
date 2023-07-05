using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.DataAccess.Models;

public class RoomOrderDetail
{
    public int Id { get; set; }

    [Required] public required string UserId { get; set; }

    [Required] public required string StripeSessionId { get; set; }

    [Required] public DateTime CheckInDate { get; set; }

    [Required] public DateTime CheckOutDate { get; set; }

    [Required] public DateTime ActualCheckInDate { get; set; }

    [Required] public DateTime ActualCheckOutDate { get; set; }

    [Required] public long TotalCost { get; set; }

    [Required] public int RoomId { get; set; }

    [Required] public bool IsPaymentSuccessful { get; set; } = false;

    [Required] public string Name { get; set; } = string.Empty;

    [Required] public required string Email { get; set; }

    public string Phone { get; set; } = string.Empty;

    [ForeignKey(nameof(RoomId))] public HotelRoom? HotelRoom { get; set; }

    public required string Status { get; set; } 
}