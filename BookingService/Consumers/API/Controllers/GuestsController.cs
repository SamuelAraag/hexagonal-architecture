using Application.Guests.DTOs;
using Application.Guests.Ports;
using Application.Guests.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestsController : ControllerBase
    {
        private readonly ILogger<GuestsController> _logger;
        private readonly IGuestManager _guestManager;

        public GuestsController(ILogger<GuestsController> logger, IGuestManager guestManager)
        {
            _logger = logger;
            _guestManager = guestManager;
        }

        [HttpPost]
        public async Task<ActionResult<GuestDTO>> Post(CreateGuestRequest guest)
        {
            var res = await _guestManager.Create(guest);

            //if(res.su)

        }
    }
}
