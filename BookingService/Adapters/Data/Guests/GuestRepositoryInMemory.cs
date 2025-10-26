using Domain.Entities;
using Domain.Ports;

namespace Data.Guests
{
    public class GuestRepositoryInMemory : IGuestRepository
    {
        private static readonly List<Guest> Guests = new();

        public Task<List<Guest>> GetAll()
        {
            return Task.FromResult(Guests);
        }
        
        public Task<Guest?> Get(int id)
        {
            return Task.FromResult(Guests.FirstOrDefault(g => g.Id == id));
        }

        public Task<int> Save(Guest guest)
        {
            guest.Id = GenerateNewId();

            var existing = Guests.FirstOrDefault(g => g.Id == guest.Id);
            if (existing != null)
            {
                throw new Exception("This item already exist in database");
            }

            Guests.Add(guest);
            return Task.FromResult(guest.Id);
        }

        private int GenerateNewId()
        {
            if (Guests.Count == 0)
                return 1;

            return Guests.Max(g => g.Id) + 1;
        }
    }
}
