using System;
using System.Net;
using AssignmentAPI.DTO.RoomDTO;
using AssignmentAPI.Interface;
using AssignmentAPI.Models;
using AssignmentAPI.Shared;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssignmentAPI.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly AssignmentDBContext _context;
        private readonly IMapper _mapper;

        public RoomRepository(AssignmentDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseModel<IEnumerable<RoomModel>>> GetRoomsAsync()
        {
            ResponseModel<IEnumerable<RoomModel>> responseModel = new ResponseModel<IEnumerable<RoomModel>>();

            try
            {
                List<RoomModel> getRooms = await _context.Rooms.ToListAsync();

                if (getRooms.Count == 0)
                {
                    responseModel.Code = (int)HttpStatusCode.NoContent;
                    responseModel.Data = null;
                }
                else
                {
                    responseModel.Code = (int)HttpStatusCode.OK;
                    responseModel.Data = getRooms;
                }
            }
            catch (Exception)
            {
                responseModel.Code = (int)HttpStatusCode.InternalServerError;
            }

            return responseModel;
        }

        public async Task<ActionResult<ResponseModel<IEnumerable<RoomModel>>>> GetRoomByLevel(string levelId)
        {
            ResponseModel<IEnumerable<RoomModel>> responseModel = new ResponseModel<IEnumerable<RoomModel>>();

            try
            {
                List<RoomModel> getRooms = await _context.Rooms.Where(ex => ex.LevelId == levelId).ToListAsync();

                if (getRooms.Count == 0)
                {
                    responseModel.Code = (int)HttpStatusCode.NoContent;
                    responseModel.Message = ResponseMessage.NoRecord;
                }

                responseModel.Code = (int)HttpStatusCode.OK;
                responseModel.Data = getRooms;

            }
            catch (Exception)
            {
                responseModel.Code = (int)HttpStatusCode.InternalServerError;
                responseModel.Message = ResponseMessage.InternalServerError;
            }
            return responseModel;
        }

        public async Task<RoomModel> GetRoomByIdAsync(string id)
        {
            return await _context.Rooms.Where(ex => ex.RoomId == id).FirstOrDefaultAsync();
        }

        public async Task<ResponseModel<RoomModel>> CreateRoomAsync(CreateRoomDTO RoomDTO)
        {
            ResponseModel<RoomModel> responseModel = new ResponseModel<RoomModel>();

            try
            {
                RoomModel Room = _mapper.Map<RoomModel>(RoomDTO);

                _context.Rooms.Add(Room);

                await _context.SaveChangesAsync();

                responseModel.Code = (int)HttpStatusCode.Created;
                responseModel.Message = ResponseMessage.SaveSuccessful;
                responseModel.Data = Room;
            }
            catch (Exception)
            {
                responseModel.Code = (int)HttpStatusCode.InternalServerError;
                responseModel.Message = ResponseMessage.InternalServerError;
            }

            return responseModel;
        }

        public async Task<ResponseModel<RoomModel>> UpdateRoomAsync(string id, UpdateRoomDTO updateRoomDTO)
        {
            ResponseModel<RoomModel> responseModel = new ResponseModel<RoomModel>();

            if (id != updateRoomDTO.RoomId)
            {
                responseModel.Code = (int)HttpStatusCode.BadRequest;
                responseModel.Message = ResponseMessage.InternalServerError;
                return responseModel;
            }

            try
            {
                RoomModel existingRoom = await _context.Rooms.FindAsync(id);

                if (existingRoom == null)
                {
                    responseModel.Code = (int)HttpStatusCode.NotFound;
                    responseModel.Message = ResponseMessage.NotFound;
                    return responseModel;
                }

                _mapper.Map(updateRoomDTO, existingRoom);

                _context.Entry(existingRoom).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                responseModel.Code = (int)HttpStatusCode.OK;
                responseModel.Message = ResponseMessage.UpdateSuccessful;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
                {
                    responseModel.Code = (int)HttpStatusCode.NotFound;
                    responseModel.Message = ResponseMessage.NotFound;
                }
                else
                {
                    responseModel.Code = (int)HttpStatusCode.InternalServerError;
                    responseModel.Message = ResponseMessage.InternalServerError;
                }
            }

            return responseModel;
        }

        public async Task<IActionResult> DeleteRoomAsync(string id)
        {
            var Room = await _context.Rooms.FindAsync(id);
            if (Room == null)
            {
                return new NotFoundResult();
            }

            _context.Rooms.Remove(Room);
            await _context.SaveChangesAsync();

            return new NoContentResult();
        }

        private bool RoomExists(string id)
        {
            return _context.Rooms.Any(e => e.RoomId == id);
        }

    }
}

