using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssignmentAPI.Models;
using AssignmentAPI.Shared;
using AssignmentAPI.DTO.GuestAccessDTO;
using AutoMapper;
using System.Net;
using AssignmentAPI.Interface;
using AssignmentAPI.Repository;

namespace AssignmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestAccessController : ControllerBase
    {
        private readonly IGuestAcessRepository _guestAcessRepository;

        public GuestAccessController(IGuestAcessRepository guestAcessRepository)
        {
            _guestAcessRepository = guestAcessRepository;
        }

        [HttpGet("GetGuestAccessByID")]
        public async Task<ActionResult<ResponseModel<IEnumerable<GuestAccessModel>>>> GetGuestAccesss()
        {
            return await _guestAcessRepository.GetGuestAccesssAsync();
        }

        [HttpGet]
        public async Task<ActionResult<GuestAccessModel>> GetGuestAccessByID(IDModel iDModel)
        {
            return await _guestAcessRepository.GetGuestAccessByIdAsync(iDModel.Id);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel<GuestAccessModel>>> PostGuestAccess(CreateGuestAccessDTO GuestAccessDTO)
        {
            return await _guestAcessRepository.CreateGuestAccessAsync(GuestAccessDTO);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseModel<GuestAccessModel>>> PutGuestAccess(UpdateGuestAccessDTO updateGuestAccessDTO)
        {
            return await _guestAcessRepository.UpdateGuestAccessAsync(updateGuestAccessDTO.GuestAccessId, updateGuestAccessDTO);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGuestAccess(IDModel iDModel)
        {
            return await _guestAcessRepository.DeleteGuestAccessAsync(iDModel.Id);
        }

    }
}
