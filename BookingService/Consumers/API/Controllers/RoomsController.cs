using Application;
using Application.Rooms.DTOs;
using Application.Rooms.Ports;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly ILogger<RoomsController> _logger;
    private readonly IRoomManager _roomManager;
    
    public RoomsController(
        ILogger<RoomsController> logger,
        IRoomManager roomManager)
    {
        _logger = logger;
        _roomManager = roomManager;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoom([FromBody] RequestCreateRoomDTO roomRequest)
    {
        var res = await _roomManager.Create(roomRequest);

        if (res.Success) return Created(res.Id.ToString(), res);

        switch (res.ErrorCode)
        {
            case ErrorCodes.RoomMissingRequiredInformation:
                return BadRequest(res);
            case ErrorCodes.RoomCouldNotBeCreated:
                return BadRequest(res);
        }

        _logger.LogError("Response with unknow ErrorCode Returned", res);
        return BadRequest(res);
    }
}