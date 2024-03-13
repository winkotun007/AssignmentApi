using System;
using AssignmentAPI.DTO.BuildingDTO;
using AssignmentAPI.Models;
using AssignmentAPI.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentAPI.Interface
{
	public interface IBuildingRepository
	{
        Task<ResponseModel<IEnumerable<BuildingModel>>> GetBuildingsAsync();
        Task<BuildingModel> GetBuildingByIdAsync(string id);
        Task<ResponseModel<BuildingModel>> CreateBuildingAsync(CreateBuildingDTO buildingDTO);
        Task<ResponseModel<BuildingModel>> UpdateBuildingAsync(string id, UpdateBuildDTO updateBuildingDTO);
        Task<IActionResult> DeleteBuildingAsync(string id);
    }
}

