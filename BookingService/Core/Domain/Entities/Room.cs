using Domain.Exceptions;
using Domain.Extensions;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool InMaintenance { get; set; }
        public Price Price { get; set; }
        public bool IsAvaliable {
            get 
            {
                if (this.InMaintenance || this.HasGuest)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
        }

        public bool HasGuest
        {
            get { return true; } //TODO: VERIFICA SE EXISTEM BOOKINGS ABERTOS PARA ESSE QUARTO
        }
        
        private void ValidateState()
        {
            if (
                !Name.HasValue() ||
                Level < 1 ||
                Price is null
            )
            {
                throw new MissingRequiredInformation();
            }

            if (!InMaintenance)
            {
                throw new CouldNotBeCreatedException();
            }
        }
    }
}
