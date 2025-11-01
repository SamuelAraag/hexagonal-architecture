using Application.Bookings.DTOs;

namespace Application.Bookings.Posts;

public interface IBookingManager
{
    Task<ResponseBookingDTOCreate> Create(RequestCreateBookingDto request);
}