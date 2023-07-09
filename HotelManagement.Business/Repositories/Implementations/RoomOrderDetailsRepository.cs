using AutoMapper;
using HotelManagement.Business.Repositories.Interfaces;
using HotelManagement.Common;
using HotelManagement.DataAccess.Data;
using HotelManagement.DataAccess.Models;
using HotelManagement.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Business.Repositories.Implementations;

public class RoomOrderDetailsRepository : IRoomOrderDetailsRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public RoomOrderDetailsRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    public async Task<RoomOrderDetailsDto?> Create(RoomOrderDetailsDto roomOrderDetailsDto)
    {
        try
        {
            roomOrderDetailsDto.CheckInDate = roomOrderDetailsDto.CheckInDate.Date;
            roomOrderDetailsDto.CheckOutDate = roomOrderDetailsDto.CheckOutDate.Date;

            var roomOrder = _mapper.Map<RoomOrderDetailsDto, RoomOrderDetail>(roomOrderDetailsDto);
            roomOrder.Status = StaticVariables.StatusPending;

            var result = await _db.RoomOrderDetails.AddAsync(roomOrder);
            await _db.SaveChangesAsync();

            return _mapper.Map<RoomOrderDetail, RoomOrderDetailsDto>(result.Entity);
        }
        catch
        {
            return null;
        }
    }

    public Task<RoomOrderDetailsDto> MarkPaymentSuccessful(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<RoomOrderDetailsDto?> GetRoomOrderDetail(int roomOrderId)
    {
        try
        {
            var roomOrderDetail = await _db.RoomOrderDetails
                .Include(a => a.HotelRoom)
                .ThenInclude(b => b.Images)
                .FirstOrDefaultAsync(a => a.Id == roomOrderId);

            if (roomOrderDetail == null) return null;

            var roomOrderDetailDto = _mapper.Map<RoomOrderDetail, RoomOrderDetailsDto>(roomOrderDetail);
            if (roomOrderDetailDto.HotelRoomDto != null)
                roomOrderDetailDto.HotelRoomDto.TotalDays =
                    roomOrderDetailDto.CheckOutDate.Subtract(roomOrderDetailDto.CheckInDate).Days;

            return roomOrderDetailDto;
        }
        catch
        {
            return null;
        }
    }

    public async Task<IEnumerable<RoomOrderDetailsDto>> GetAllRoomOrderDetails()
    {
        try
        {
            var roomOrderDetails = await _db.RoomOrderDetails
                .Include(a => a.HotelRoom).ToListAsync();

            return _mapper.Map<IEnumerable<RoomOrderDetail>, IEnumerable<RoomOrderDetailsDto>>(roomOrderDetails);

        }
        catch (Exception e)
        {
            return new List<RoomOrderDetailsDto>();
        }
    }

    public Task<bool> UpdateOrderStatus(int roomId, string status)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IsRoomBooked(int roomId, DateTime checkInDate, DateTime checkOutDate)
        => await _db.RoomOrderDetails
            .AnyAsync(a => a.RoomId == roomId && 
                           a.IsPaymentSuccessful &&
                           (checkInDate < a.CheckOutDate && checkInDate.Date > a.CheckInDate) || 
                           (checkOutDate.Date > a.CheckInDate.Date && checkInDate.Date < a.CheckInDate.Date));

}