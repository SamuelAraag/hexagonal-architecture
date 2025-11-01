using Domain.Entities;

namespace Application.Bookings.DTOs;

public class ResponseBookingDTOCreate : Response
{
    public Booking Data { get; set; }
}