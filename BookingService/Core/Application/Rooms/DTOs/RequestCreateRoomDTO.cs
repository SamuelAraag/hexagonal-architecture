using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Rooms.DTOs;

public class RequestCreateRoomDTO
{
    public string Name { get; set; }
    public int Level { get; set; }
    public bool InMaintenance { get; set; }
    public decimal Price { get; set; }
    public AcceptedCurrencies Currency { get; set; }

    public static Room MapToEntity(RequestCreateRoomDTO roomRoomDto)
    {
        return new Room
        {
            Name = roomRoomDto.Name,
            Level = roomRoomDto.Level,
            InMaintenance = roomRoomDto.InMaintenance,
            Price = new Price
            {
                Currency = roomRoomDto.Currency,
                Value = roomRoomDto.Price,
            }
        };
    }
}