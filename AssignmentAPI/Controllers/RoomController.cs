using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssignmentAPI.Models;
using AssignmentAPI.Shared;
using AssignmentAPI.DTO.RoomDTO;
using AutoMapper;
using System.Net;
using AssignmentAPI.Interface;

namespace AssignmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;

        public RoomsController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel<IEnumerable<RoomModel>>>> GetRooms()
        {
            return await _roomRepository.GetRoomsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoomModel>> GetRoom(string id)
        {
            return await _roomRepository.GetRoomByIdAsync(id);
        }

        [Route("GetRoomsByLevel/{levelId}")]
        [HttpGet]
        public async Task<ActionResult<ResponseModel<IEnumerable<RoomModel>>>> GetRoomsByLevel(string levelId)
        {
            return await _roomRepository.GetRoomByLevel(levelId);
        }



        [HttpPost]
        public async Task<ActionResult<ResponseModel<RoomModel>>> PostRoom(CreateRoomDTO RoomDTO)
        {
            return await _roomRepository.CreateRoomAsync(RoomDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseModel<RoomModel>>> PutRoom(string id, UpdateRoomDTO updateRoomDTO)
        {
            return await _roomRepository.UpdateRoomAsync(id, updateRoomDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(string id)
        {
            return await _roomRepository.DeleteRoomAsync(id);
        }
    }
}
