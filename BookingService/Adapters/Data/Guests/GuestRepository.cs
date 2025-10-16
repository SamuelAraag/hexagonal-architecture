using Domain.Entities;
using Domain.Ports;

namespace Data.Guests
{
    public class GuestRepository : IGuestRepository
    {
        private readonly HotelDbContext _hotelDbContext;

        public GuestRepository(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;    
        }

        public Task<Guest> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Save(Guest guest)
        {
            _hotelDbContext.Guests.Add(guest);
            await _hotelDbContext.SaveChangesAsync();
            return guest.Id;
        }
    }
}
