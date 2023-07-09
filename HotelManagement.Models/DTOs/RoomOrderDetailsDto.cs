using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models.DTOs;

public class RoomOrderDetailsDto
{
    public int Id { get; set; }

    [Required] public string UserId { get; set; } = string.Empty;

    [Required] public string StripeSessionId { get; set; } = string.Empty;

    [Required] public DateTime CheckInDate { get; set; }

    [Required] public DateTime CheckOutDate { get; set; }

    [Required] public DateTime ActualCheckInDate { get; set; }

    [Required] public DateTime ActualCheckOutDate { get; set; }

    [Required] public long TotalCost { get; set; }

    [Required] public int RoomId { get; set; }

    [Required] public bool IsPaymentSuccessful { get; set; } = false;

    [Required] public string Name { get; set; } = string.Empty;

    [Required] public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    [ForeignKey(nameof(RoomId))] public HotelRoomDto? HotelRoomDto { get; set; }

    public string Status { get; set; } = string.Empty;
}