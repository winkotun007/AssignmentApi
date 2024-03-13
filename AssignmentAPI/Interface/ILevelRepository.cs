using System;
using AssignmentAPI.DTO.LevelDTO;
using AssignmentAPI.Models;
using AssignmentAPI.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentAPI.Interface
{

    public interface ILevelRepository
    {
        Task<ResponseModel<IEnumerable<LevelModel>>> GetLevelsAsync();
        Task<LevelModel> GetLevelByIdAsync(string id);
        Task<ResponseModel<LevelModel>> CreateLevelAsync(CreateLevelDTO levelDTO);
        Task<ResponseModel<LevelModel>> UpdateLevelAsync(string id, UpdateLevelDTO updateLevelDTO);
        Task<IActionResult> DeleteLevelAsync(string id);
        Task<ActionResult<ResponseModel<IEnumerable<LevelModel>>>> GetLevelByBuilding(string buildingId);
    }
}


