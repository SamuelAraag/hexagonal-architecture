using Application.Guests.DTOs;

namespace Application.Guests.Ports
{
    public interface IGuestManager
    {
        Task<ResponseGuestDTOCreate> Create(RequestCreateGuestDTO request);
        Task<ResponseGuestGet> GetById(int guestId);
        Task<List<ResponseGuestGet>> GetAll();
    }
}
