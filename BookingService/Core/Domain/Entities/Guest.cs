using Domain.Exceptions;
using Domain.Ports;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Guest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public PersonId DocumentId { get; set; }

        public async Task<int> Save(IGuestRepository guestRepository)
        {
            this.ValidateState();

            return await guestRepository.Save(this);
        }

        private void ValidateState()
        {
            if(
                DocumentId is null || 
                DocumentId.IdNumber.Length < 4 || 
                DocumentId.DocumentType == 0
                )
            {
                throw new InvalidPersonDocumentIdException();
            }

            if (
                Name is null ||
                Surname is null ||
                Email is null
                )
            {
                throw new MissingRequiredInformation();
            }

            if (!Utils.IsValidEmail(Email))
            {
                throw new InvalidEmailException();
            }
        }
    }
}
