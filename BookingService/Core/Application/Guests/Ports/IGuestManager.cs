using Application.Guests.Requests;
using Application.Guests.Responses;

namespace Application.Guests.Ports
{
    public interface IGuestManager
    {
        Task<GuestResponse> Create(CreateGuestRequest request); //Vamos usar o conceito de DTO (Data Transfer Object)
    }
}
