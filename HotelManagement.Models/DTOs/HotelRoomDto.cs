using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models.DTOs;

public class HotelRoomDto
{
    public int Id { get; set; }

    [Display(Name = "عنوان اتاق")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Name { get; set; } = default!;

    [Display(Name = "ظرفیت")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public int Occupancy { get; set; }

    [Display(Name = "قیمت")]
    [Range(1000000, 30000000, ErrorMessage = "{0} باید بین {1} و {2} باشد")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public double RegularRate { get; set; }

    [Display(Name = "جزئیات")]
    public string Details { get; set; } = default!;

    [Display(Name = "زیربنا")]
    public string SqFt { get; set; } = default!;

    public double TotalDays { get; set; }
    public double TotalAmount { get; set; }

    public virtual ICollection<HotelRoomImageDto> Images { get; set; } = default!;
    public virtual List<string> ImageUrls { get; set; } = default!;
}