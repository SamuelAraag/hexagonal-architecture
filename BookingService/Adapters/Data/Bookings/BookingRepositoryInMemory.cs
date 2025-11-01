using Domain.Entities;
using Domain.Ports;

namespace Data.Bookings;

public class BookingRepositoryInMemory : IBookingRepository
{
    private static readonly List<Booking> Bookings = new();
    
    public Task<Booking?> GetById(int id)
    {
        return Task.FromResult(Bookings.FirstOrDefault(g => g.Id == id));
    }

    public Task<int> Create(Booking booking)
    {
        booking.Id = GenerateNewId();

        var existing = Bookings.FirstOrDefault(g => g.Id == booking.Id);
        if (existing != null)
        {
            throw new Exception("This item already exist in database");
        }

        Bookings.Add(booking);
        return Task.FromResult(booking.Id);
    }

    public Task<List<Booking>> GetRelatedRoom(int roomId)
    {
        return Task.FromResult(Bookings.Where(booking => booking.Room.Id == roomId).ToList());
    }

    private static int GenerateNewId()
    {
        if (Bookings.Count == 0)
            return 1;

        return Bookings.Max(g => g.Id) + 1;
    }
}