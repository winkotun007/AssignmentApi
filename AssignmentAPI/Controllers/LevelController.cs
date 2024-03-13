using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssignmentAPI.Models;
using AssignmentAPI.Shared;
using AssignmentAPI.DTO.LevelDTO;
using AutoMapper;
using System.Net;
using AssignmentAPI.Interface;
using AssignmentAPI.Repository;

namespace AssignmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LevelsController : ControllerBase
    {
        private readonly ILevelRepository _levelRepository;

        public LevelsController(ILevelRepository levelRepository)
        {
            _levelRepository = levelRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel<IEnumerable<LevelModel>>>> GetLevels()
        {
            return await _levelRepository.GetLevelsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LevelModel>> GetLevel(string id)
        {
            return await _levelRepository.GetLevelByIdAsync(id);
        }

        [Route("GetLevelsByBuilding/{levelId}")]
        [HttpGet]
        public async Task<ActionResult<ResponseModel<IEnumerable<LevelModel>>>> GetLevelsByBuilding(string levelId)
        {
            return await _levelRepository.GetLevelByBuilding(levelId);
        }



        [HttpPost]
        public async Task<ActionResult<ResponseModel<LevelModel>>> PostLevel(CreateLevelDTO levelDTO)
        {
            return await _levelRepository.CreateLevelAsync(levelDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseModel<LevelModel>>> PutLevel(string id, UpdateLevelDTO updateLevelDTO)
        {
            return await _levelRepository.UpdateLevelAsync(id,updateLevelDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLevel(string id)
        {
            return await _levelRepository.DeleteLevelAsync(id);
        }

    }
}
