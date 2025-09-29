using Domain.Entities;
using Domain.Enums;

namespace DomainTests
{
    public class Tests
    {
        [Fact]
        public void ShouldAlwaysStartWithCreatedStatus()
        {
            var booking = new Booking();

            Assert.Equal(StatusBooking.Created, booking.CurrentStatus);
        }

        [Fact]
        public void ShouldSetStatusToPaidWhenPayingForABookingWithCreateStatus()
        {
            var booking = new Booking();

            booking.ChangeState(ActionState.Pay);

            Assert.Equal(StatusBooking.Paid, booking.CurrentStatus);
        }

        [Fact]
        public void ShouldSetStatusToCanceldWhenCancelingABookingWithCreatedStatus()
        {
            var booking = new Booking();

            booking.ChangeState(ActionState.Cancel);

            Assert.Equal(StatusBooking.Canceled, booking.CurrentStatus);
        }
    }
}
