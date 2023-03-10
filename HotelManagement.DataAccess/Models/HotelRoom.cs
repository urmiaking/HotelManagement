using System.ComponentModel.DataAnnotations;

namespace HotelManagement.DataAccess.Models;

public class HotelRoom
{
    [Key]
    public int Id { get; set; }
    [Required]
    public required string Name { get; set; }
    [Required]
    public int Occupancy { get; set; }
    [Required]
    public double RegularRate { get; set; }
    public string? Details { get; set; }
    public string? SqFt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.Now;
    public string? UpdatedBy { get; set; }
    public DateTime UpdateDate { get; set; }
}