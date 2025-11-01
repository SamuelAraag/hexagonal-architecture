using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Guests.DTOs
{
    public class RequestCreateGuestDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string IdNumber { get; set; }
        public int IdTypeCode { get; set; }

        public static Guest MapToEntity(RequestCreateGuestDto createGuestDto)
        {
            return new Guest
            {
                Name = createGuestDto.Name,
                Email = createGuestDto.Email,
                Surname = createGuestDto.Surname,
                DocumentId = new PersonId
                {
                    IdNumber = createGuestDto.IdNumber,
                    DocumentType = (DocumentType)createGuestDto.IdTypeCode
                }
            };
        }
    }
}
