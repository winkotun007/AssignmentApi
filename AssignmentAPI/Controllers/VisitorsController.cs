using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssignmentAPI.Models;
using AssignmentAPI.Shared;
using AssignmentAPI.DTO.VisitorsDTO;
using AutoMapper;
using System.Net;
using AssignmentAPI.Interface;
namespace AssignmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorsController : ControllerBase
    {
        private readonly IVisitorsRepository _visitorRepository;

        public VisitorsController(IVisitorsRepository visitorsRepository)
        {
            _visitorRepository = visitorsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel<IEnumerable<GetVisitorsDTO>>>> GetVisitorss()
        {
            return await _visitorRepository.GetVisitorsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VisitorsModel>> GetVisitors(string id)
        {
            return await _visitorRepository.GetVisitorsByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel<VisitorsModel>>> PostVisitors(CreateVisitorsDTO VisitorsDTO)
        {
            return await _visitorRepository.CreateVisitorsAsync(VisitorsDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseModel<VisitorsModel>>> PutVisitors(string id, UpdateVisitorsDTO updateVisitorsDTO)
        {
            return await _visitorRepository.UpdateVisitorsAsync(id, updateVisitorsDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisitors(string id)
        {
            return await _visitorRepository.DeleteVisitorsAsync(id);
        }
    }
}
