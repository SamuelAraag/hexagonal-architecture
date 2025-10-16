using Application.Guests.DTOs;
using Application.Guests.Ports;
using Application.Guests.Requests;
using Application.Guests.Responses;
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

        public async Task<GuestResponse> Create(CreateGuestRequest request)
        {
            var guest = GuestDTO.MapToEntity(request.Data);

            request.Data.Id = await _guestRepository.Save(guest);

            return new GuestResponse
            {
                Success = false,
                ErrorCode = ErrorCodes.CouldNotStoreData,
                Message = "There was an error when saving guest",
            };
        }
    }
}
