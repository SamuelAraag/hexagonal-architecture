using Domain.ValueObjects;

namespace Application.Rooms.DTOs;

public class ResponseCreateRoomDto : Response
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public bool InMaintenance { get; set; }
    public Price Price { get; set; }
}