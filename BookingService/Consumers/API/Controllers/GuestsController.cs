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

        public GuestsController(
            ILogger<GuestsController> logger, 
            IGuestManager guestManager
            )
        {
            _logger = logger;
            _guestManager = guestManager;
        }

        [HttpPost]
        public async Task<ActionResult<RequestGuestDTOCreate>> Post(RequestGuestDTOCreate guest)
        {
            var request = new CreateGuestRequest
            {
                Data = guest
            };

            var res = await _guestManager.Create(request);

            if (res.Success) return Created(res.Id.ToString(), res);

            //Pra cada erro, posso ser tratado individualmente
            if (res.ErrorCode == Application.ErrorCodes.NotFound) { return BadRequest(res); }; 
            if (res.ErrorCode == Application.ErrorCodes.InvalidPersonId) { return BadRequest(res); };
            if (res.ErrorCode == Application.ErrorCodes.MissingRequiredInformation) { return BadRequest(res); };
            if (res.ErrorCode == Application.ErrorCodes.InvalidEmail) { return BadRequest(res); };
            if (res.ErrorCode == Application.ErrorCodes.CouldNotStoreData) { return BadRequest(res); };

            _logger.LogError("Response with unknown ErrorCode Returned", res);
            return BadRequest();
        }
    }
}
