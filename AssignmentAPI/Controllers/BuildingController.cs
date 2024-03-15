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


        [HttpGet("GetBuildingByID")]
        public async Task<ActionResult<BuildingModel>> GetBuildingByID(IDModel iDModel)
        {
            _logger.LogInformation("Get Building By ID");
            return await _buildingRepository.GetBuildingByIdAsync(iDModel.Id);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel<BuildingModel>>> PostBuilding(CreateBuildingDTO buildingDTO)
        {
            _logger.LogInformation("Cretate New Building.");
            return await _buildingRepository.CreateBuildingAsync(buildingDTO);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseModel<BuildingModel>>> PutBuilding(UpdateBuildDTO updateBuildDTO)
        {
            _logger.LogInformation("Update Existing Buidling.");
            return await _buildingRepository.UpdateBuildingAsync(updateBuildDTO.BuildingId, updateBuildDTO);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBuilding(IDModel iDModel)
        {
            _logger.LogInformation("Delete By Buidling ID.");
            return await _buildingRepository.DeleteBuildingAsync(iDModel.Id);
        }
    }
}
