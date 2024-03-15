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

        [HttpGet("GetRoomByID")]
        public async Task<ActionResult<RoomModel>> GetRoomByID(IDModel iDModel)
        {
            return await _roomRepository.GetRoomByIdAsync(iDModel.Id);
        }

        [HttpPost("GetRoomsByLevel")]
        public async Task<ActionResult<ResponseModel<IEnumerable<RoomModel>>>> GetRoomsByLevel(IDModel iDModel)
        {
            return await _roomRepository.GetRoomByLevel(iDModel.Id);
        }



        [HttpPost]
        public async Task<ActionResult<ResponseModel<RoomModel>>> PostRoom(CreateRoomDTO RoomDTO)
        {
            return await _roomRepository.CreateRoomAsync(RoomDTO);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseModel<RoomModel>>> PutRoom(UpdateRoomDTO updateRoomDTO)
        {
            return await _roomRepository.UpdateRoomAsync(updateRoomDTO.RoomId, updateRoomDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(IDModel iDModel)
        {
            return await _roomRepository.DeleteRoomAsync(iDModel.Id);
        }
    }
}
