using Application.Rooms.DTOs;
using Application.Rooms.Ports;
using Domain.Ports;

namespace Application.Rooms;

public class RoomManager : IRoomManager
{
    private readonly IRoomRepository _roomRepository;

    public RoomManager(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }
    
    public async Task<ResponseCreateRoomDTO> Create(RequestCreateRoomDTO roomRoomDto)
    {
        try
        {
            var room = RequestCreateRoomDTO.MapToEntity(roomRoomDto);
        
            var idRoom = await _roomRepository.Save(room);

            return new ResponseCreateRoomDTO
            {
                Id = idRoom,
                Name = room.Name,
                Level = room.Level,
                InMaintenance = room.InMaintenance,
                Price = room.Price,
            };
        }
        catch (InvalidPersonDocumentIdException e)
        {
            return new ResponseGuestDTOCreate
            {
                Success = false,
                ErrorCode = ErrorCodes.InvalidPersonId,
                Message = "Error with the Person Document Id: " + e.ToString(),
            };
        }
        catch (MissingRequiredInformation e)
        {
            return new ResponseGuestDTOCreate
            {
                Success = false,
                ErrorCode = ErrorCodes.MissingRequiredInformation,
                Message = "Error with Missing Required Information: " + e.ToString(),
            };

        }
        catch (InvalidEmailException e)
        {
            return new ResponseCreateRoomDTO
            {
                Success = false,
                ErrorCode = ErrorCodes.InvalidEmail,
                Message = "Error with Email information: " + e.ToString(),
            };
        }
        catch (Exception e)
        {
            return new ResponseCreateRoomDTO()
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