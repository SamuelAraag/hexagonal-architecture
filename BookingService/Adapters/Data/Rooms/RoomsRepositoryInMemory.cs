using Domain.Entities;
using Domain.Ports;

namespace Data.Rooms;

public class RoomsRepositoryInMemory : IRoomRepository
{
    private static readonly List<Room> Rooms = new();

    public Task<List<Room>> GetAll()
    {
        return Task.FromResult(Rooms);
    }
        
    public Task<Room?> Get(int id)
    {
        return Task.FromResult(Rooms.FirstOrDefault(g => g.Id == id));
    }

    public Task<int> Save(Room room)
    {
        room.Id = GenerateNewId();

        var existing = Rooms.FirstOrDefault(g => g.Id == room.Id);
        if (existing != null)
        {
            throw new Exception("This item already exist in database");
        }

        Rooms.Add(room);
        return Task.FromResult(room.Id);
    }

    private static int GenerateNewId()
    {
        if (Rooms.Count == 0)
            return 1;

        return Rooms.Max(g => g.Id) + 1;
    }
}