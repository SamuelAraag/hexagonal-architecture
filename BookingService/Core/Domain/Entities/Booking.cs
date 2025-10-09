using Domain.Enums;

namespace Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime PlaceAt { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Guest Guest { get; set; }
        public Room Room { get; set; }
        private StatusBooking Status { get; set; } = StatusBooking.Created;
        public StatusBooking CurrentStatus 
        { 
            get 
            { 
                return this.Status; 
            } 
        }

        public void ChangeState(ActionState action)
        {
            this.Status = (this.Status, action) switch
            {
                (StatusBooking.Created,     ActionState.Pay)        => StatusBooking.Paid,
                (StatusBooking.Created,     ActionState.Cancel)     => StatusBooking.Canceled,
                (StatusBooking.Paid,        ActionState.Finish)     => StatusBooking.Finished,
                (StatusBooking.Paid,        ActionState.Refound)    => StatusBooking.Refounded,
                (StatusBooking.Canceled,    ActionState.Reopen)     => StatusBooking.Created,
                _ => this.Status
            };
        }
    }
}
