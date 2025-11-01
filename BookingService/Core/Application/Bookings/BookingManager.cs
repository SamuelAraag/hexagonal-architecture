using Application.Bookings.DTOs;
using Application.Bookings.Posts;
using Domain.Ports;

namespace Application.Bookings;

public class BookingManager : IBookingManager
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IGuestRepository _guestRepository;
    private readonly IRoomRepository _roomRepository;
    
    public BookingManager(
        IBookingRepository bookingRepository,
        IGuestRepository guestRepository,
        IRoomRepository roomRepository
        )
    {
        _bookingRepository = bookingRepository;
        _guestRepository = guestRepository;
        _roomRepository = roomRepository;
    }
    public async Task<ResponseBookingDTOCreate> Create(RequestCreateBookingDto request)
    {
        var bookingResult = RequestCreateBookingDto.MapToEntity(request);
        
        bookingResult.Guest = await _guestRepository.Get(request.GuestId)
            ?? throw new Exception($"Error to find Guest: [{request.GuestId}]");
        
        bookingResult.Room = await _roomRepository.GetAggregate(request.RoomId)
                              ?? throw new Exception($"Error to find Room: [{request.RoomId}]");
        
        bookingResult.Guest.Validate();
        if (!bookingResult.Room.IsAvailable)
        {
            throw new Exception($"The room is not available to booking: [{bookingResult.Room.Name}]");
        }
        
        bookingResult.Id = await _bookingRepository.Create(bookingResult);
        var response = new ResponseBookingDTOCreate()
        {
            Data = bookingResult,
            Success = true,
        };

        return response;
    }
}