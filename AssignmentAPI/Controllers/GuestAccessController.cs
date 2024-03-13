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

        [HttpGet]
        public async Task<ActionResult<ResponseModel<IEnumerable<GuestAccessModel>>>> GetGuestAccesss()
        {
            return await _guestAcessRepository.GetGuestAccesssAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GuestAccessModel>> GetGuestAccess(string id)
        {
            return await _guestAcessRepository.GetGuestAccessByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel<GuestAccessModel>>> PostGuestAccess(CreateGuestAccessDTO GuestAccessDTO)
        {
            return await _guestAcessRepository.CreateGuestAccessAsync(GuestAccessDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseModel<GuestAccessModel>>> PutGuestAccess(string id, UpdateGuestAccessDTO updateGuestAccessDTO)
        {
            return await _guestAcessRepository.UpdateGuestAccessAsync(id, updateGuestAccessDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuestAccess(string id)
        {
            return await _guestAcessRepository.DeleteGuestAccessAsync(id);
        }

    }
}
