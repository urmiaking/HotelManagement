using System.ComponentModel.DataAnnotations;

namespace HotelManagement.DataAccess.Models;

public class HotelAmenity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Timing { get; set; }
    public string? Icon { get; set; }

    [Required]
    public required string Description { get; set; }
}