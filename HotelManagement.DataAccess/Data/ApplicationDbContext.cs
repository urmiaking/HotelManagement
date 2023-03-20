﻿using HotelManagement.DataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.DataAccess.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }

    public required DbSet<HotelRoom> HotelRooms { get; set; }
    public required DbSet<HotelRoomImage> HotelRoomsImage { get; set; }
    public required DbSet<HotelAmenity> HotelAmenities { get; set; }
}

