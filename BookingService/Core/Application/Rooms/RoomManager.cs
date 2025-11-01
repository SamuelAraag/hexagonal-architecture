using Application.Rooms.DTOs;
using Application.Rooms.Ports;
using Domain.Exceptions;
using Domain.Ports;

namespace Application.Rooms;

public class RoomManager : IRoomManager
{
    private readonly IRoomRepository _roomRepository;

    public RoomManager(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }
    
    public async Task<ResponseCreateRoomDto> Create(RequestCreateRoomDto roomRoomDto)
    {
        try
        {
            var room = RequestCreateRoomDto.MapToEntity(roomRoomDto);
        
            var idRoom = await _roomRepository.Save(room);

            return new ResponseCreateRoomDto
            {
                Id = idRoom,
                Name = room.Name,
                Level = room.Level,
                InMaintenance = room.InMaintenance,
                Price = room.Price,
                Success = true
            };
        }
        catch (InvalidRoomDataException)
        {
            return new ResponseCreateRoomDto()
            {
                Success = false,
                ErrorCode = ErrorCodes.CouldNotStoreData,
                Message = "There was an error when saving Room: ",
            };
        }
        catch (Exception e)
        {
            return new ResponseCreateRoomDto()
            {
                Success = false,
                ErrorCode = ErrorCodes.CouldNotStoreData,
                Message = "There was an error when saving Room: " + e.ToString(),
            };
        }
    }

    public async Task<ResponseRoomGet> GetById(int roomId)
    {
        var room = await _roomRepository.Get(roomId);

        if (room is null)
        {
            return new ResponseRoomGet
            {
                Success = false,
                ErrorCode = ErrorCodes.NotFound,
                Message = $"No Room record was found with the giver ID: {roomId}",
            };
        }

        return new ResponseRoomGet
        {
            Id = room.Id,
            Name = room.Name,
            Level = room.Level,
            InMaintenance = room.InMaintenance,
            Price = room.Price,
            Success = true
        };
    }

    public async Task<List<ResponseRoomGet>> GetAll()
    {
        var rooms = await _roomRepository.GetAll();

        return rooms.Select(room => new ResponseRoomGet
        {
            Id = room.Id,
            Name = room.Name,
            InMaintenance = room.InMaintenance,
            Level = room.Level,
            Success = true,
        }).ToList();
    }
}