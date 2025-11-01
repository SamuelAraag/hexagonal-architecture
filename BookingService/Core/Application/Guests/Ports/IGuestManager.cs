using Application.Guests.DTOs;

namespace Application.Guests.Ports
{
    public interface IGuestManager
    {
        Task<ResponseGuestDTOCreate> Create(RequestCreateGuestDto request);
        Task<ResponseGuestGet> GetById(int guestId);
        Task<List<ResponseGuestGet>> GetAll();
    }
}
