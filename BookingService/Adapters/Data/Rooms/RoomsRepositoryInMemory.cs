using Domain.Entities;
using Domain.Exceptions;
using Domain.Ports;

namespace Data.Rooms;

public class RoomsRepositoryInMemory : IRoomRepository
{
    private readonly IBookingRepository _bookingRepository;
    private static readonly List<Room> Rooms = new();

    public RoomsRepositoryInMemory(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public Task<List<Room>> GetAll()
    {
        return Task.FromResult(Rooms);
    }
    
    public Task<Room?> Get(int id)
    {
        return Task.FromResult(Rooms.FirstOrDefault(g => g.Id == id));
    }
    
    public async Task<Room?> GetAggregate(int id)
    {
        var roomRespose = Rooms.FirstOrDefault(g => g.Id == id);
        var bookings = await _bookingRepository.GetRelatedRoom(roomRespose.Id);
        
        roomRespose.Bookings = bookings;

        return roomRespose;
    }

    public Task<int> Save(Room room)
    {
        room.Id = GenerateNewId();

        var existing = Rooms.FirstOrDefault(g => g.Id == room.Id || g.Name == room.Name);
        ValidateState(room);
        
        if (existing != null)
        {
            throw new Exception("This item already exist in database");
        }

        Rooms.Add(room);
        return Task.FromResult(room.Id);
    }

    private static void ValidateState(Room room)
    {
        if (string.IsNullOrWhiteSpace(room.Name))
        {
            throw new InvalidRoomDataException();
        }
    }

    private static int GenerateNewId()
    {
        if (Rooms.Count == 0)
            return 1;

        return Rooms.Max(g => g.Id) + 1;
    }
}