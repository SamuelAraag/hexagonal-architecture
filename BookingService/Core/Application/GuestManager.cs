using Application.Guests.DTOs;
using Application.Guests.Ports;
using Application.Guests.Requests;
using Domain.Ports;

namespace Application
{
    public class GuestManager : IGuestManager
    {
        private readonly IGuestRepository _guestRepository;

        public GuestManager(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }

        public async Task<ResponseGuestDTOCreate> Create(CreateGuestRequest request)
        {
            try
            {
                var guest = RequestGuestDTOCreate.MapToEntity(request.Data);

                var idResponse = await guest.Save(_guestRepository);

                return new ResponseGuestDTOCreate
                {
                    Email = request.Data.Email,
                    IdNumber = request.Data.IdNumber,
                    Name = request.Data.Name,
                    Surname = request.Data.Surname,
                    IdTypeCode = request.Data.IdTypeCode,
                    Success = true,
                };
            }
            catch (Exception)
            {
                return new ResponseGuestDTOCreate
                {
                    Success = false,
                    ErrorCode = ErrorCodes.CouldNotStoreData,
                    Message = "There was an error when saving guest",
                };
            }

        }
    }
}
