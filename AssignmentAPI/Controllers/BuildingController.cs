using Microsoft.AspNetCore.Mvc;
using AssignmentAPI.Models;
using AssignmentAPI.Shared;
using AssignmentAPI.DTO.BuildingDTO;
using AssignmentAPI.Interface;
using Microsoft.AspNetCore.Authorization;

namespace AssignmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private readonly IBuildingRepository _buildingRepository;
        public BuildingController(IBuildingRepository buildingRepository)
        {
            _buildingRepository = buildingRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel<IEnumerable<BuildingModel>>>> GetBuilding()
        {
            return await _buildingRepository.GetBuildingsAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<BuildingModel>> GetBuilding(string id)
        {
            return await _buildingRepository.GetBuildingByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel<BuildingModel>>> PostBuilding(CreateBuildingDTO buildingDTO)
        {
            return await _buildingRepository.CreateBuildingAsync(buildingDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseModel<BuildingModel>>> PutBuilding(string id,UpdateBuildDTO updateBuildDTO)
        {
            return await _buildingRepository.UpdateBuildingAsync(id, updateBuildDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuilding(string id)
        {
            return await _buildingRepository.DeleteBuildingAsync(id);
        }
    }
}
