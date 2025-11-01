using Domain.Entities;

namespace Domain.Ports;

public interface IBookingRepository
{
    Task<Booking?> GetById(int id);
    Task<int> Create(Booking booking);
    Task<List<Booking>> GetRelatedRoom(int roomId);
}