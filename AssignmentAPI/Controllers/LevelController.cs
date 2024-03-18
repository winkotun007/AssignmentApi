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

        [HttpGet("GetLevelByID")]
        public async Task<ActionResult<LevelModel>> GetLevelByID(IDModel iDModel)
        {
            return await _levelRepository.GetLevelByIdAsync(iDModel.Id);
        }


        [HttpPost("GetLevelsByBuilding")]
        public async Task<ActionResult<ResponseModel<IEnumerable<LevelModel>>>> GetLevelsByBuilding(IDModel iDModel)
        {
            return await _levelRepository.GetLevelByBuilding(iDModel.Id);
        }



        [HttpPost]
        public async Task<ActionResult<ResponseModel<LevelModel>>> PostLevel(CreateLevelDTO levelDTO)
        {
            return await _levelRepository.CreateLevelAsync(levelDTO);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseModel<LevelModel>>> PutLevel(UpdateLevelDTO updateLevelDTO)
        {
            return await _levelRepository.UpdateLevelAsync(updateLevelDTO.BuildingId,updateLevelDTO);
        }

        [HttpDelete]
        public async Task<ResponseDeleteModel> DeleteLevel(IDModel iDModel)
        {
            return await _levelRepository.DeleteLevelAsync(iDModel.Id);
        }

    }
}
