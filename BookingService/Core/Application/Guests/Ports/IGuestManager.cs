using Application.Guests.DTOs;
using Application.Guests.Requests;

namespace Application.Guests.Ports
{
    public interface IGuestManager
    {
        Task<ResponseGuestDTOCreate> Create(CreateGuestRequest request); //Vamos usar o conceito de DTO (Data Transfer Object)
    }
}
