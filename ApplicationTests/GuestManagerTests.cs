using Application;
using Application.Guests.DTOs;
using Application.Guests.Requests;
using Domain.Entities;
using Domain.Ports;
using Moq;

namespace ApplicationTests
{
    public class GuestManagerTests
    {
        private readonly GuestManager _guestManager;

        public GuestManagerTests()
        {
            var fake = new Mock<IGuestRepository>();
            fake.Setup(x => x.Save(It.IsAny<Guest>())).Returns(Task.FromResult(11));

            _guestManager = new GuestManager(fake.Object); //Object é a classe criada
        }

        [Fact]
        public async Task WhenCreateGuest_HasTo_CreateSucess()
        {
            var guestDTO = new RequestGuestDTOCreate
            {
                Name = "Sam",
                Surname = "Santos",
                Email = "samuel@santos.com",
                IdNumber = "abca",
                IdTypeCode = 4,
            };

            var request = new CreateGuestRequest
            {
                Data = guestDTO
            };

            var res = await _guestManager.Create(request);

            Assert.NotNull(res);
            Assert.True(res.Success);
        }
    }
}
