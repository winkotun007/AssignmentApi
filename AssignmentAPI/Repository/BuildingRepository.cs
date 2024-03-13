using System;
using AssignmentAPI.Shared;
using AssignmentAPI.Interface;
using Microsoft.AspNetCore;
using AutoMapper;
using AssignmentAPI.Models;
using Microsoft.EntityFrameworkCore;
using AssignmentAPI.DTO.BuildingDTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AssignmentAPI.Repository
{

	public class BuildingRepository : IBuildingRepository
    { 
        private readonly AssignmentDBContext _context;
        private readonly IMapper _mapper;
        private readonly IUserIdProvider _userIdProvider;
        public BuildingRepository(AssignmentDBContext context,IMapper mapper,IUserIdProvider userIdProvider)
        {
            _userIdProvider = userIdProvider;
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseModel<IEnumerable<BuildingModel>>> GetBuildingsAsync()
        {
            ResponseModel<IEnumerable<BuildingModel>> responseModel = new ResponseModel<IEnumerable<BuildingModel>>();

            try
            {
                List<BuildingModel> getBuildings = await _context.Buildings.ToListAsync();

                if (getBuildings.Count == 0)
                {
                    responseModel.Code = (int)HttpStatusCode.NoContent;
                    responseModel.Data = null;
                }
                else
                {
                    responseModel.Code = (int)HttpStatusCode.OK;
                    responseModel.Data = getBuildings;
                }
            }
            catch (Exception)
            {
                responseModel.Code = (int)HttpStatusCode.InternalServerError;
            }

            return responseModel;
        }

        public async Task<BuildingModel> GetBuildingByIdAsync(string id)
        {
            return await _context.Buildings.Where(ex => ex.BuildingId == id).FirstOrDefaultAsync() ;
        }

        public async Task<ResponseModel<BuildingModel>> CreateBuildingAsync(CreateBuildingDTO buildingDTO)
        {
            ResponseModel<BuildingModel> responseModel = new ResponseModel<BuildingModel>();

            try
            {
                BuildingModel building = _mapper.Map<BuildingModel>(buildingDTO);

                _context.Buildings.Add(building);

                await _context.SaveChangesAsync();

                responseModel.Code = (int)HttpStatusCode.Created;
                responseModel.Data = building;
            }
            catch (Exception)
            {
                responseModel.Code = (int)HttpStatusCode.InternalServerError;
            }

            return responseModel;
        }

        public async Task<ResponseModel<BuildingModel>> UpdateBuildingAsync(string id, UpdateBuildDTO updateBuildingDTO)
        {
            ResponseModel<BuildingModel> responseModel = new ResponseModel<BuildingModel>();

            if (id != updateBuildingDTO.BuildingId)
            {
                responseModel.Code = (int)HttpStatusCode.BadRequest;
                responseModel.Message = ResponseMessage.InternalServerError;
                return responseModel;
            }

            try
            {

                BuildingModel existingBuilding = await _context.Buildings.FindAsync(id);

                if (existingBuilding == null)
                {
                    responseModel.Code = (int)HttpStatusCode.NotFound;
                    responseModel.Message = ResponseMessage.NotFound;
                    return responseModel;
                }

                // Map the properties from updateBuildingDTO to existingBuilding
                _mapper.Map(updateBuildingDTO, existingBuilding);

                _context.Entry(existingBuilding).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                responseModel.Code = (int)HttpStatusCode.OK;
                responseModel.Message = ResponseMessage.UpdateSuccessful;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuildingExists(id))
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

        public async Task<IActionResult> DeleteBuildingAsync(string id)
        {
            var i =_userIdProvider.GetUserId();
            var building = await _context.Buildings.FindAsync(id);
            if (building == null)
            {
                return new NotFoundResult();
            }

            _context.Buildings.Remove(building);
            await _context.SaveChangesAsync();

            return new NoContentResult();
        }

        private bool BuildingExists(string id)
        {
            return _context.Buildings.Any(e => e.BuildingId == id);
        }
    }
}

