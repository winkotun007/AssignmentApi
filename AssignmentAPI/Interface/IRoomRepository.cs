using System;
using AssignmentAPI.DTO.RoomDTO;
using AssignmentAPI.Models;
using AssignmentAPI.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentAPI.Interface
{

    public interface IRoomRepository
    {
        Task<ResponseModel<IEnumerable<RoomModel>>> GetRoomsAsync();
        Task<RoomModel> GetRoomByIdAsync(string id);
        Task<ResponseModel<RoomModel>> CreateRoomAsync(CreateRoomDTO RoomDTO);
        Task<ResponseModel<RoomModel>> UpdateRoomAsync(string id, UpdateRoomDTO updateRoomDTO);
        Task<IActionResult> DeleteRoomAsync(string id);
        Task<ActionResult<ResponseModel<IEnumerable<RoomModel>>>> GetRoomByLevel(string levelId);
    }
}

