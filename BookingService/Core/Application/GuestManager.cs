using Application.Guests.DTOs;
using Application.Guests.Ports;
using Application.Guests.Requests;
using Application.Guests.Responses;
using Domain.Exceptions;
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
            var guest = await _guestRepository.GetAll();

            var guestResponse = guest?.Select(guest =>
            {
                return new ResponseGuestGet
                {
                    Email = guest.Email,
                    Id = guest.Id,
                    Name = guest.Name,
                    Surname = guest.Surname,
                    Success = true
                };
            });

            return guestResponse?.ToList() ?? new ();
        }
    }
}
