using Domain.Exceptions;
using Domain.Extensions;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool InMaintenance { get; set; }
        public Price Price { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        
        public bool IsAvailable {
            get
            {
                if (InMaintenance || HasGuest) {
                    return false;
                };
                
                return true;
            }
        }

        public bool HasGuest
        {
            get
            {
                var notAvailableStatus = new List<Enums.StatusBooking>
                {
                    Enums.StatusBooking.Created,
                    Enums.StatusBooking.Paid
                };
                
                return Bookings.Any(booking => booking.Room.Id == Id && notAvailableStatus.Contains(booking.CurrentStatus));
            }
        }
        
        private void ValidateState()
        {
            if (
                !Name.HasValue() ||
                Level < 1 ||
                Price is null
            )
            {
                throw new MissingRequiredInformation();
            }

            if (!InMaintenance)
            {
                throw new CouldNotBeCreatedException();
            }
        }
    }
}
