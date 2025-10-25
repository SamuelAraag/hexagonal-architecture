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

            _guestManager = new GuestManager(fake.Object); //'Object' é a classe criada
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

        [Theory]
        [InlineData("a")]
        [InlineData("b")]
        [InlineData("")]
        [InlineData(null)]
        public async Task WhenCreateGuest_HasTo_ErrorInvalidDocument(string docNumber)
        {
            var guestDTO = new RequestGuestDTOCreate
            {
                Name = "Sam",
                Surname = "Santos",
                Email = "samuel@santos.com",
                IdNumber = docNumber,
                IdTypeCode = 4,
            };

            var request = new CreateGuestRequest
            {
                Data = guestDTO
            };

            var res = await _guestManager.Create(request);

            Assert.NotNull(res);
            Assert.False(res.Success);
            Assert.Equal(ErrorCodes.InvalidPersonId, res.ErrorCode);
            Assert.Contains("Error with the Person Document Id:", res.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public async Task WhenCreateGuest_HasTo_ErrorInvalidName(string name)
        {
            var guestDTO = new RequestGuestDTOCreate
            {
                Name = name,
                Surname = "Santos",
                Email = "samuel@santos.com",
                IdNumber = "docNumber",
                IdTypeCode = 4,
            };

            var request = new CreateGuestRequest
            {
                Data = guestDTO
            };

            var res = await _guestManager.Create(request);

            Assert.NotNull(res);
            Assert.False(res.Success);
            Assert.Equal(ErrorCodes.MissingRequiredInformation, res.ErrorCode);
            Assert.Contains("Error with Missing Required Information:", res.Message);
        }
        
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        [InlineData("samuel")]
        [InlineData("samuel@")]
        [InlineData("@santos.com")]
        [InlineData("samuel@santos.")]
        [InlineData("samuel@santos,com")]
        public async Task WhenCreateGuest_HasTo_ErrorInvalidEmail(string email)
        {
            var guestDTO = new RequestGuestDTOCreate
            {
                Name = "Samuel",
                Surname = "Santos",
                Email = email,
                IdNumber = "docNumber",
                IdTypeCode = 4,
            };

            var request = new CreateGuestRequest
            {
                Data = guestDTO
            };

            var res = await _guestManager.Create(request);

            Assert.NotNull(res);
            Assert.False(res.Success);
            Assert.Equal(ErrorCodes.InvalidEmail, res.ErrorCode);
            Assert.Contains("Error with Email information: ", res.Message);
        }
        
        [Theory]
        [InlineData("samuel@santos.com")]
        [InlineData("samuel.santos@gmail.com")]
        [InlineData("samuel+test@company.co")]
        public async Task WhenCreateGuest_HasTo_SuccessValidEmail(string email)
        {
            var guestDTO = new RequestGuestDTOCreate
            {
                Name = "Samuel",
                Surname = "Santos",
                Email = email,
                IdNumber = "docNumber",
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
