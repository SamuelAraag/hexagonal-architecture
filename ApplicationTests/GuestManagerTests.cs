using Application;
using Application.Guests;
using Application.Guests.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Ports;
using Domain.ValueObjects;
using Moq;

namespace ApplicationTests
{
    public class GuestManagerTests
    {
        private GuestManager _guestManager;

        public GuestManagerTests()
        {
            var fake = new Mock<IGuestRepository>();
            fake.Setup(x => x.Save(It.IsAny<Guest>())).Returns(Task.FromResult(11));
            fake.Setup(x => x.GetAll()).Returns(Task.FromResult(GetGuests()));

            _guestManager = new GuestManager(fake.Object); //'Object' é a classe criada
        }

        [Fact]
        public async Task WhenCreateGuest_HasTo_CreateSucess()
        {
            var guestDTO = new RequestCreateGuestDto
            {
                Name = "Sam",
                Surname = "Santos",
                Email = "samuel@santos.com",
                IdNumber = "abca",
                IdTypeCode = 4,
            };

            var res = await _guestManager.Create(guestDTO);

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
            var guestDTO = new RequestCreateGuestDto
            {
                Name = "Sam",
                Surname = "Santos",
                Email = "samuel@santos.com",
                IdNumber = docNumber,
                IdTypeCode = 4,
            };

            var res = await _guestManager.Create(guestDTO);

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
            var guestDTO = new RequestCreateGuestDto
            {
                Name = name,
                Surname = "Santos",
                Email = "samuel@santos.com",
                IdNumber = "docNumber",
                IdTypeCode = 4,
            };

            var res = await _guestManager.Create(guestDTO);

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
            var guestDTO = new RequestCreateGuestDto
            {
                Name = "Samuel",
                Surname = "Santos",
                Email = email,
                IdNumber = "docNumber",
                IdTypeCode = 4,
            };

            var res = await _guestManager.Create(guestDTO);

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
            var guestDTO = new RequestCreateGuestDto
            {
                Name = "Samuel",
                Surname = "Santos",
                Email = email,
                IdNumber = "docNumber",
                IdTypeCode = 4,
            };

            var res = await _guestManager.Create(guestDTO);

            Assert.NotNull(res);
            Assert.True(res.Success);
        }
        
        [Fact]
        public async Task WhenGetAll_HasTo_ReturnListOfGuests()
        {
            var res = await _guestManager.GetAll();

            Assert.NotNull(res);
            Assert.Equal(200, res.First().Id);
        }
        
        [Fact]
        public async Task WhenGetAll_HasTo_ReturnListEmptyOfGuests()
        {
            var guestsEmpty = new List<ResponseGuestGet>();
            var fake = new Mock<IGuestRepository>();

            fake.Setup(x => x.GetAll()).Returns(Task.FromResult(GetGuests(true)));

            _guestManager = new GuestManager(fake.Object);
            
            var res = await _guestManager.GetAll();

            Assert.NotNull(res);
            Assert.Empty(res);
        }
        
        [Theory]
        [InlineData(null)]
        [InlineData(200)]
        [InlineData(0)]
        [InlineData(1)]

        public async Task WhenGetById_HasTo_ReturnGuestWithTheSameId(int  guestId)
        {
            var res = await _guestManager.GetById(guestId);

            Assert.NotNull(res);
            Assert.False(res.Success);
        }
        
        [Fact]
        public async Task WhenGetById_WithInvalidId_ReturnError()
        {
            var guestsEmpty = new List<ResponseGuestGet>();
            var fake = new Mock<IGuestRepository>();

            fake.Setup(x => x.Get(It.IsAny<int>())).Returns(Task.FromResult(GetGuests().First()));

            _guestManager = new GuestManager(fake.Object);
            
            var res = await _guestManager.GetById(11);

            Assert.NotNull(res);
            Assert.Equal(200, res.Id);
        }

        private static List<Guest> GetGuests(bool isEmpty = false)
        {
            var guests = new List<Guest>();
            if (isEmpty)
            {
                return guests;
            }

            guests.Add(new Guest()
            {
                Id = 200,
                Email = "sam@santos",
                Name = "Samuel",
                Surname = "Santos",
                DocumentId = new PersonId
                {
                    DocumentType = DocumentType.Passport,
                    IdNumber = "1234"
                }
            });

            return guests;
        }
    }
}
