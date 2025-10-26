using Application.Guests.DTOs;
using Application.Guests.Ports;
using Domain.Exceptions;
using Domain.Ports;

namespace Application.Guests
{
    public class GuestManager : IGuestManager
    {
        private readonly IGuestRepository _guestRepository;

        public GuestManager(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }

        public async Task<ResponseGuestDTOCreate> Create(RequestCreateGuestDTO request)
        {
            try
            {
                var guest = RequestCreateGuestDTO.MapToEntity(request);

                var idResponse = await guest.Save(_guestRepository);

                return new ResponseGuestDTOCreate
                {
                    Id = idResponse,
                    Email = request.Email,
                    IdNumber = request.IdNumber,
                    Name = request.Name,
                    Surname = request.Surname,
                    IdTypeCode = request.IdTypeCode,
                    Success = true,
                };
            }
            catch (InvalidPersonDocumentIdException e)
            {
                return new ResponseGuestDTOCreate
                {
                    Success = false,
                    ErrorCode = ErrorCodes.InvalidPersonId,
                    Message = "Error with the Person Document Id: " + e.ToString(),
                };
            }
            catch (MissingRequiredInformation e)
            {
                return new ResponseGuestDTOCreate
                {
                    Success = false,
                    ErrorCode = ErrorCodes.MissingRequiredInformation,
                    Message = "Error with Missing Required Information: " + e.ToString(),
                };

            }
            catch (InvalidEmailException e)
            {
                return new ResponseGuestDTOCreate
                {
                    Success = false,
                    ErrorCode = ErrorCodes.InvalidEmail,
                    Message = "Error with Email information: " + e.ToString(),
                };
            }
            catch (Exception e)
            {
                return new ResponseGuestDTOCreate
                {
                    Success = false,
                    ErrorCode = ErrorCodes.CouldNotStoreData,
                    Message = "There was an error when saving guest: " + e.ToString(),
                };
            }
        }
        
        public async Task<ResponseGuestGet> GetById(int guestId)
        {
            var guest = await _guestRepository.Get(guestId);

            if (guest is null)
            {
                return new ResponseGuestGet
                {
                    Success = false,
                    ErrorCode = ErrorCodes.NotFound,
                    Message = $"No guest record was found with the giver ID: {guestId}",
                };
            }

            return new ResponseGuestGet
            {
                Email = guest.Email,
                Id = guest.Id,
                Name = guest.Name,
                Surname = guest.Surname,
                Success = true
            };
        }
        
        public async Task<List<ResponseGuestGet>> GetAll()
        {
            var guests = await _guestRepository.GetAll();

            return guests.Select(guest => new ResponseGuestGet
            {
                Email = guest.Email,
                Id = guest.Id,
                Name = guest.Name,
                Surname = guest.Surname,
                Success = true
            }).ToList();
        }
    }
}
