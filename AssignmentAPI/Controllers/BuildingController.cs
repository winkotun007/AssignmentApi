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
        private readonly ILogger<BuildingController> _logger;
        public BuildingController(IBuildingRepository buildingRepository,ILogger<BuildingController> logger)
        {
            _buildingRepository = buildingRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel<IEnumerable<BuildingModel>>>> GetBuilding()
        {
            _logger.LogInformation("Get Building");

            return await _buildingRepository.GetBuildingsAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<BuildingModel>> GetBuilding(string id)
        {
            _logger.LogInformation("Get Building By ID");
            return await _buildingRepository.GetBuildingByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel<BuildingModel>>> PostBuilding(CreateBuildingDTO buildingDTO)
        {
            _logger.LogInformation("Cretate New Building.");
            return await _buildingRepository.CreateBuildingAsync(buildingDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseModel<BuildingModel>>> PutBuilding(string id,UpdateBuildDTO updateBuildDTO)
        {
            _logger.LogInformation("Update Existing Buidling.");
            return await _buildingRepository.UpdateBuildingAsync(id, updateBuildDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuilding(string id)
        {
            _logger.LogInformation("Delete By Buidling ID.");
            return await _buildingRepository.DeleteBuildingAsync(id);
        }
    }
}
