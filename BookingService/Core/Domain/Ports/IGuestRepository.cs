using Domain.Entities;

namespace Domain.Ports
{
    public interface IGuestRepository
    {
        Task<List<Guest>> GetAll();
        Task<Guest?> Get(int id);
        Task<int> Save(Guest guest);
    }
}
