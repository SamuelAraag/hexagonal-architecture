using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Guests.DTOs
{
    public class RequestGuestDTOCreate
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string IdNumber { get; set; }
        public int IdTypeCode { get; set; }

        public static Guest MapToEntity(RequestGuestDTOCreate guestDTO)
        {
            return new Guest
            {
                Name = guestDTO.Name,
                Email = guestDTO.Email,
                Surname = guestDTO.Surname,
                DocumentId = new PersonId
                {
                    IdNumber = guestDTO.IdNumber,
                    DocumentType = (DocumentType)guestDTO.IdTypeCode
                }
            };
        }
    }
}
