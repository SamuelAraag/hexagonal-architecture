using Domain.Entities;

namespace Domain.Ports;

public interface IRoomRepository
{
    Task<List<Room>> GetAll();
    Task<Room?> Get(int id);
    Task<int> Save(Room guest);
}