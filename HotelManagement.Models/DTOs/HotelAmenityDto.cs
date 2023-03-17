using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HotelManagement.Models.DTOs;

public class HotelAmenityDto
{
    public int Id { get; set; }

    [Display(Name = "عنوان امکانات")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Name { get; set; } = default!;

    [Display(Name = "شرح")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Description { get; set; } = default!;

    [Display(Name = "زمان بندی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Timing { get; set; } = default!;

    [Display(Name = "نماد")]
    public string? Icon { get; set; }
}