using HotelManagement.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.DataAccess.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }

    public required DbSet<HotelRoom> HotelRooms { get; set; }
    public required DbSet<HotelRoomImage> HotelRoomsImage { get; set; }
    public required DbSet<HotelAmenity> HotelAmenities { get; set; }
    public required DbSet<ApplicationUser> ApplicationUser { get; set; }
    public required DbSet<RoomOrderDetail> RoomOrderDetails { get; set; }
}