using Application.Guests.DTOs;

namespace Application.Guests.Responses
{
    public class GuestResponse : Response
    {
        public GuestDTO Data { get; set; }
    }
}
