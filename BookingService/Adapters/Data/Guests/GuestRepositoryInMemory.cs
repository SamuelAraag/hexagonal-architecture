using Domain.Entities;
using Domain.Ports;

namespace Data.Guests
{
    public class GuestRepositoryInMemory : IGuestRepository
    {
        private static readonly List<Guest> guests = new();

        public Task<Guest> Get(int id)
        {
            return Task.FromResult(guests.FirstOrDefault(g => g.Id == id));
        }

        public Task<int> Save(Guest guest)
        {
            guest.Id = GenerateNewId();

            var existing = guests.FirstOrDefault(g => g.Id == guest.Id);
            if (existing != null)
            {
                throw new Exception("This item already exist in database");
            }

            guests.Add(guest);
            return Task.FromResult(guest.Id);
        }

        private int GenerateNewId()
        {
            if (guests.Count == 0)
                return 1;

            return guests.Max(g => g.Id) + 1;
        }
    }
}
