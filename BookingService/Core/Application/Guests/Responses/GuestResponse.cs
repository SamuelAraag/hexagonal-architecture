using Application.Guests.DTOs;

namespace Application.Guests.Responses
{
    public class GuestResponse : Response
    {
        public ResponseGuestDTOCreate Data { get; set; }
    }
}
