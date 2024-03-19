using System;
using AssignmentAPI.DTO.VisitorsDTO;
using AssignmentAPI.Interface;
using AssignmentAPI.Models;
using AssignmentAPI.Shared;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AssignmentAPI.Repository
{

    public class VisitorsRepository : IVisitorsRepository
    {
        private readonly AssignmentDBContext _context;
        private readonly IMapper _mapper;

        public VisitorsRepository(AssignmentDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseModel<IEnumerable<GetVisitorsDTO>>> GetVisitorsAsync()
        {
            ResponseModel<IEnumerable<GetVisitorsDTO>> responseModel = new ResponseModel<IEnumerable<GetVisitorsDTO>>();

            try
            {

                List<VisitorsModel> getVisitors = await _context.Visitors
                    .Include(v => v.Building)
                    .Include(v => v.Level)
                    .Include(v => v.Room)
                    .ToListAsync();


                // Manually project the related properties
               List<GetVisitorsDTO> getVisitorsList = getVisitors.Select(dto => new GetVisitorsDTO
                {
                    // Map properties directly from VisitorsModel
                    VisitorId = dto.VisitorId,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    NRICNumber = dto.NRICNumber,
                    PlateNumber = dto.PlateNumber,
                    CompanyName = dto.CompanyName,
                    Designation = dto.Designation,
                    isStayHomeNotice = dto.isStayHomeNotice,
                    isConfirmed14Day = dto.isConfirmed14Day,
                    isFever = dto.isFever,
                    isAcknowledged = dto.isAcknowledged,
                    BuildingName = dto.Building?.BuildingName,
                    LevelName = dto.Level?.LevelName,
                    RoomName = dto.Room?.RoomName
                }).ToList();



                if (getVisitorsList.Count == 0)
                {
                    responseModel.Code = (int)HttpStatusCode.NoContent;
                    responseModel.Data = null;
                }
                else
                {
                    responseModel.Code = (int)HttpStatusCode.OK;
                    responseModel.Data = getVisitorsList;
                }
            }
            catch (Exception)
            {
                responseModel.Code = (int)HttpStatusCode.InternalServerError;
            }

            return responseModel;
        }

        public async Task<VisitorsModel> GetVisitorsByIdAsync(string id)
        {
            return await _context.Visitors.Include(v => v.Building)
                                          .Include(v => v.Level)
                                          .Include(v => v.Room)
                                          .FirstOrDefaultAsync(ex => ex.VisitorId == id);
        }

        public async Task<ResponseModel<VisitorsModel>> CreateVisitorsAsync(CreateVisitorsDTO VisitorsDTO)
        {
            ResponseModel<VisitorsModel> responseModel = new ResponseModel<VisitorsModel>();

            try
            {
                VisitorsModel Visitors = _mapper.Map<VisitorsModel>(VisitorsDTO);

                _context.Visitors.Add(Visitors);

                await _context.SaveChangesAsync();

                responseModel.Code = (int)HttpStatusCode.Created;
                responseModel.Message = ResponseMessage.SaveSuccessful;
                responseModel.Data = Visitors;
            }
            catch (Exception)
            {
                responseModel.Code = (int)HttpStatusCode.InternalServerError;
                responseModel.Message = ResponseMessage.InternalServerError;
            }

            return responseModel;
        }

        public async Task<ResponseModel<VisitorsModel>> UpdateVisitorsAsync(string id, UpdateVisitorsDTO updateVisitorsDTO)
        {
            ResponseModel<VisitorsModel> responseModel = new ResponseModel<VisitorsModel>();

            if (id != updateVisitorsDTO.VisitorId)
            {
                responseModel.Code = (int)HttpStatusCode.BadRequest;
                responseModel.Message = ResponseMessage.InternalServerError;
                return responseModel;
            }

            try
            {
                VisitorsModel existingVisitors = await _context.Visitors.FindAsync(id);

                if (existingVisitors == null)
                {
                    responseModel.Code = (int)HttpStatusCode.NotFound;
                    responseModel.Message = ResponseMessage.NotFound;
                    return responseModel;
                }

                _mapper.Map(updateVisitorsDTO, existingVisitors);

                _context.Entry(existingVisitors).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                responseModel.Code = (int)HttpStatusCode.OK;
                responseModel.Message = ResponseMessage.UpdateSuccessful;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitorsExists(id))
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

        public async Task<ActionResult<ResponseModel<VisitorsModel>>> DeleteVisitorsAsync(string id)
        {
            ResponseModel<VisitorsModel> responseModel = new ResponseModel<VisitorsModel>();

            try
            {
                var Visitors = await _context.Visitors.FindAsync(id);


                if (Visitors == null)
                {
                    responseModel.Code = (int)HttpStatusCode.NoContent;
                    responseModel.Data = null;
                    responseModel.Message = ResponseMessage.NotFound;
                    return responseModel;
                }

                _context.Visitors.Remove(Visitors);
                await _context.SaveChangesAsync();

                responseModel.Code = (int)HttpStatusCode.OK;
                responseModel.Message = ResponseMessage.DeleteSuccessful;
                responseModel.Data = null;
                return responseModel;
            }
            catch (Exception)
            {
                responseModel.Code = (int)HttpStatusCode.InternalServerError;
                responseModel.Message = ResponseMessage.InternalServerError;
                responseModel.Data = null;
            }
            return responseModel;
        }

        private bool VisitorsExists(string id)
        {
            return _context.Visitors.Any(e => e.VisitorId == id);
        }

    }
}

