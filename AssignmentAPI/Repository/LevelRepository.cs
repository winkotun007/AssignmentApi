using System;
using System.Net;
using AssignmentAPI.DTO.LevelDTO;
using AssignmentAPI.Interface;
using AssignmentAPI.Models;
using AssignmentAPI.Shared;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssignmentAPI.Repository
{
	public class LevelRepository : ILevelRepository
    {
        private readonly AssignmentDBContext _context;
        private readonly IMapper _mapper;

        public LevelRepository(AssignmentDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseModel<IEnumerable<LevelModel>>> GetLevelsAsync()
        {
            ResponseModel<IEnumerable<LevelModel>> responseModel = new ResponseModel<IEnumerable<LevelModel>>();

            try
            {
                List<LevelModel> getLevels = await _context.levels.ToListAsync();


                if (getLevels.Count == 0)
                {
                    responseModel.Code = (int)HttpStatusCode.NoContent;
                    responseModel.Data = null;
                }
                else
                {
                    responseModel.Code = (int)HttpStatusCode.OK;
                    responseModel.Data = getLevels;
                }
            }
            catch (Exception)
            {
                responseModel.Code = (int)HttpStatusCode.InternalServerError;
            }

            return responseModel;
        }

        public async Task<ActionResult<ResponseModel<IEnumerable<LevelModel>>>> GetLevelByBuilding(string buildingId)
        {
            ResponseModel<IEnumerable<LevelModel>> responseModel = new ResponseModel<IEnumerable<LevelModel>>();

            try
            {
                List<LevelModel> getLevels = await _context.levels.Where(ex => ex.BuildingId == buildingId).ToListAsync();

                if (getLevels.Count == 0)
                {
                    responseModel.Code = (int)HttpStatusCode.NoContent;
                    responseModel.Message = ResponseMessage.NoRecord;
                }

                responseModel.Code = (int)HttpStatusCode.OK;
                responseModel.Data = getLevels;

            }
            catch (Exception)
            {
                responseModel.Code = (int)HttpStatusCode.InternalServerError;
                responseModel.Message = ResponseMessage.InternalServerError;
            }
            return responseModel;
        }

        public async Task<LevelModel> GetLevelByIdAsync(string id)
        {
            return await _context.levels.Where(ex => ex.LevelId == id).FirstOrDefaultAsync();
        }

        public async Task<ResponseModel<LevelModel>> CreateLevelAsync(CreateLevelDTO levelDTO)
        {
            ResponseModel<LevelModel> responseModel = new ResponseModel<LevelModel>();

            try
            {
                LevelModel level = _mapper.Map<LevelModel>(levelDTO);

                _context.levels.Add(level);

                await _context.SaveChangesAsync();

                responseModel.Code = (int)HttpStatusCode.Created;
                responseModel.Message = ResponseMessage.SaveSuccessful;
                responseModel.Data = level;
            }
            catch (Exception)
            {
                responseModel.Code = (int)HttpStatusCode.InternalServerError;
                responseModel.Message = ResponseMessage.InternalServerError;
            }

            return responseModel;
        }

        public async Task<ResponseModel<LevelModel>> UpdateLevelAsync(string id, UpdateLevelDTO updateLevelDTO)
        {
            ResponseModel<LevelModel> responseModel = new ResponseModel<LevelModel>();

            if (id != updateLevelDTO.LevelId)
            {
                responseModel.Code = (int)HttpStatusCode.BadRequest;
                responseModel.Message = ResponseMessage.InternalServerError;
                return responseModel;
            }

            try
            {
                LevelModel existingLevel = await _context.levels.FindAsync(id);

                if (existingLevel == null)
                {
                    responseModel.Code = (int)HttpStatusCode.NotFound;
                    responseModel.Message = ResponseMessage.NotFound;
                    return responseModel;
                }

                _mapper.Map(updateLevelDTO, existingLevel);

                _context.Entry(existingLevel).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                responseModel.Code = (int)HttpStatusCode.OK;
                responseModel.Message = ResponseMessage.UpdateSuccessful;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LevelExists(id))
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

        public async Task<IActionResult> DeleteLevelAsync(string id)
        {
            var level = await _context.levels.FindAsync(id);
            if (level == null)
            {
                return new NotFoundResult();
            }

            _context.levels.Remove(level);
            await _context.SaveChangesAsync();

            return new NoContentResult();
        }

        private bool LevelExists(string id)
        {
            return _context.levels.Any(e => e.LevelId == id);
        }

    }
}

