using Domain.Enums;

namespace Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime PlaceAt { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        private StatusBooking Status { get; set; }
        public StatusBooking CurrentStatus 
        { 
            get 
            { 
                return this.Status; 
            } 
        }
    }
}
