using Application.Rooms.DTOs;

namespace Application.Rooms.Ports;

public interface IRoomManager
{
    Task<ResponseCreateRoomDTO> Create(RequestCreateRoomDTO roomRoomDto);
    Task<ResponseRoomGet> GetById(int roomId);
    Task<List<ResponseRoomGet>> GetAll();
}