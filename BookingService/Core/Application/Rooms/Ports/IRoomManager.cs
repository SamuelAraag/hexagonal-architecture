using Application.Rooms.DTOs;

namespace Application.Rooms.Ports;

public interface IRoomManager
{
    Task<ResponseCreateRoomDto> Create(RequestCreateRoomDto roomRoomDto);
    Task<ResponseRoomGet> GetById(int roomId);
    Task<List<ResponseRoomGet>> GetAll();
}