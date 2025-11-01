using Application;
using Application.Guests.DTOs;
using Application.Guests.Ports;
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
        
        [HttpGet]
        public async Task<ActionResult<List<ResponseGuestGet>>> GetAll()
        {
            var res = await _guestManager.GetAll();

            return Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult<RequestCreateGuestDto>> Post(RequestCreateGuestDto createGuest)
        {
            var res = await _guestManager.Create(createGuest);

            if (res.Success) return Created(res.Id.ToString(), res);

            //Pra cada erro, posso ser tratado individualmente
            if (res.ErrorCode == ErrorCodes.NotFound) { return BadRequest(res); }; 
            if (res.ErrorCode == ErrorCodes.InvalidPersonId) { return BadRequest(res); };
            if (res.ErrorCode == ErrorCodes.MissingRequiredInformation) { return BadRequest(res); };
            if (res.ErrorCode == ErrorCodes.InvalidEmail) { return BadRequest(res); };
            if (res.ErrorCode == ErrorCodes.CouldNotStoreData) { return BadRequest(res); };

            _logger.LogError("Response with unknown ErrorCode Returned", res);
            return BadRequest();
        }
        
        [HttpGet("{guestId:int}")]
        public async Task<ActionResult<RequestCreateGuestDto>> Get(int guestId)
        {
            var res = await _guestManager.GetById(guestId);

            if (res.Success) return Ok(res);
            
            return BadRequest(res);
        }
    }
}
