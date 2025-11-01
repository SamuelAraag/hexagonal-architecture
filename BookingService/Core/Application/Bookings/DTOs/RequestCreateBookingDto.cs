using Domain.Entities;
using Domain.Enums;

namespace Application.Bookings.DTOs;

public class RequestCreateBookingDto
{
    public DateTime PlaceAt { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int GuestId { get; set; }
    public int RoomId { get; set; }
    private StatusBooking Status { get; set; }

    public static Booking MapToEntity(RequestCreateBookingDto request)
    {
        return new Booking()
        {
            Start = request.Start,
            End = request.End,
            PlaceAt = request.PlaceAt,
            Guest = new Guest() { Id = request.GuestId },
            Room = new Room() { Id = request.RoomId },
        };
    }
}