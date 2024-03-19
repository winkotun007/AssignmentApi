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

        [HttpPost("GetVisitorByID")]
        public async Task<ActionResult<VisitorsModel>> GetVisitorsByID(IDModel iDModel)
        {
            /
            return await _visitorRepository.GetVisitorsByIdAsync(iDModel.Id);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel<VisitorsModel>>> PostVisitors(CreateVisitorsDTO VisitorsDTO)
        {
            return await _visitorRepository.CreateVisitorsAsync(VisitorsDTO);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseModel<VisitorsModel>>> PutVisitors(UpdateVisitorsDTO updateVisitorsDTO)
        {
            return await _visitorRepository.UpdateVisitorsAsync(updateVisitorsDTO.VisitorId, updateVisitorsDTO);
        }

        [HttpDelete]
        public async Task<ActionResult<ResponseModel<VisitorsModel>>> DeleteVisitors(IDModel iDModel)
        {
            return await _visitorRepository.DeleteVisitorsAsync(iDModel.Id);
        }
    }
}
