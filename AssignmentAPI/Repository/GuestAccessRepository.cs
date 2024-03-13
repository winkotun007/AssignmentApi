using System;
using System.Net;
using AssignmentAPI.DTO.GuestAccessDTO;
using AssignmentAPI.Interface;
using AssignmentAPI.Models;
using AssignmentAPI.Shared;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssignmentAPI.Repository
{
    public class GuestAccesssRepository : IGuestAcessRepository
    {
        private readonly AssignmentDBContext _context;
        private readonly IMapper _mapper;

        public GuestAccesssRepository(AssignmentDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseModel<IEnumerable<GuestAccessModel>>> GetGuestAccesssAsync()
        {
            ResponseModel<IEnumerable<GuestAccessModel>> responseModel = new ResponseModel<IEnumerable<GuestAccessModel>>();

            try
            {
                List<GuestAccessModel> getGuestAccesss = await _context.GuestAccess.ToListAsync();

                if (getGuestAccesss.Count == 0)
                {
                    responseModel.Code = (int)HttpStatusCode.NoContent;
                    responseModel.Data = null;
                }
                else
                {
                    responseModel.Code = (int)HttpStatusCode.OK;
                    responseModel.Data = getGuestAccesss;
                }
            }
            catch (Exception)
            {
                responseModel.Code = (int)HttpStatusCode.InternalServerError;
            }

            return responseModel;
        }

        public async Task<bool> isAccessByPath(string Path,string Method)
        {
            if(!string.IsNullOrEmpty(Path))
            {
                GuestAccessModel getGuestAccesss = new GuestAccessModel();
            
            if (!string.IsNullOrEmpty(Method))
                {
                    switch (Method)
                    {
                        case "GET":
                            getGuestAccesss= await _context.GuestAccess
                                                    .Where(ex => ex.Path.Contains(Path) && ex.isGetAccess==true).FirstOrDefaultAsync();
                            break;

                        case "POST":
                            getGuestAccesss = await _context.GuestAccess
                                                    .Where(ex => ex.Path.Contains(Path) && ex.isPostAccess==true).FirstOrDefaultAsync();
                            break;

                        case "DELETE":
                            getGuestAccesss = await _context.GuestAccess
                                                    .Where(ex => ex.Path.Contains( Path) && ex.isDeleteAccess ==true).FirstOrDefaultAsync();
                            break;

                        case "PUT":
                            getGuestAccesss = await _context.GuestAccess
                                                    .Where(ex => ex.Path.Contains(Path) && ex.isPutAccess==true).FirstOrDefaultAsync();
                            break;
                        default:
                            getGuestAccesss = null;
                            break;
                    }
                }
                
                if(getGuestAccesss==null)
                {
                    return false;
                }
                else
                {
                    return true;
                }

                return false;

            }
            return false;
        }

        public async Task<ActionResult<ResponseModel<IEnumerable<GuestAccessModel>>>> GetGuestAccessByBuilding(string guessAccessId)
        {
            ResponseModel<IEnumerable<GuestAccessModel>> responseModel = new ResponseModel<IEnumerable<GuestAccessModel>>();

            try
            {
                List<GuestAccessModel> getGuestAccesss = await _context.GuestAccess.Where(ex => ex.GuestAccessId == guessAccessId).ToListAsync();

                if (getGuestAccesss.Count == 0)
                {
                    responseModel.Code = (int)HttpStatusCode.NoContent;
                    responseModel.Message = ResponseMessage.NoRecord;
                }

                responseModel.Code = (int)HttpStatusCode.OK;
                responseModel.Data = getGuestAccesss;

            }
            catch (Exception)
            {
                responseModel.Code = (int)HttpStatusCode.InternalServerError;
                responseModel.Message = ResponseMessage.InternalServerError;
            }
            return responseModel;
        }

        public async Task<GuestAccessModel> GetGuestAccessByIdAsync(string id)
        {
            return await _context.GuestAccess.Where(ex => ex.GuestAccessId == id).FirstOrDefaultAsync();
        }

        public async Task<ResponseModel<GuestAccessModel>> CreateGuestAccessAsync(CreateGuestAccessDTO GuestAccessDTO)
        {
            ResponseModel<GuestAccessModel> responseModel = new ResponseModel<GuestAccessModel>();

            try
            {
                GuestAccessModel GuestAccess = _mapper.Map<GuestAccessModel>(GuestAccessDTO);

                _context.GuestAccess.Add(GuestAccess);

                await _context.SaveChangesAsync();

                responseModel.Code = (int)HttpStatusCode.Created;
                responseModel.Message = ResponseMessage.SaveSuccessful;
                responseModel.Data = GuestAccess;
            }
            catch (Exception)
            {
                responseModel.Code = (int)HttpStatusCode.InternalServerError;
                responseModel.Message = ResponseMessage.InternalServerError;
            }

            return responseModel;
        }

        public async Task<ResponseModel<GuestAccessModel>> UpdateGuestAccessAsync(string id, UpdateGuestAccessDTO updateGuestAccessDTO)
        {
            ResponseModel<GuestAccessModel> responseModel = new ResponseModel<GuestAccessModel>();

            if (id != updateGuestAccessDTO.GuestAccessId)
            {
                responseModel.Code = (int)HttpStatusCode.BadRequest;
                responseModel.Message = ResponseMessage.InternalServerError;
                return responseModel;
            }

            try
            {
                GuestAccessModel existingGuestAccess = await _context.GuestAccess.FindAsync(id);

                if (existingGuestAccess == null)
                {
                    responseModel.Code = (int)HttpStatusCode.NotFound;
                    responseModel.Message = ResponseMessage.NotFound;
                    return responseModel;
                }

                _mapper.Map(updateGuestAccessDTO, existingGuestAccess);

                _context.Entry(existingGuestAccess).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                responseModel.Code = (int)HttpStatusCode.OK;
                responseModel.Message = ResponseMessage.UpdateSuccessful;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuestAccessExists(id))
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

        public async Task<IActionResult> DeleteGuestAccessAsync(string id)
        {
            var GuestAccess = await _context.GuestAccess.FindAsync(id);
            if (GuestAccess == null)
            {
                return new NotFoundResult();
            }

            _context.GuestAccess.Remove(GuestAccess);
            await _context.SaveChangesAsync();

            return new NoContentResult();
        }

        private bool GuestAccessExists(string id)
        {
            return _context.GuestAccess.Any(e => e.GuestAccessId == id);
        }

    }
}

