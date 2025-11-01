using Application.Bookings.DTOs;
using Application.Bookings.Posts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController  : ControllerBase
{
    private readonly IBookingManager _bookingManager;

    public BookingsController(IBookingManager bookingManager)
    {
        _bookingManager = bookingManager;
    }

    [HttpPost]
    public async Task<IActionResult> Create(RequestCreateBookingDto  bookingDto)
    {
        var res = await _bookingManager.Create(bookingDto);

        if (res.Success) return Ok(res);
        
        return BadRequest(res);
    }
}