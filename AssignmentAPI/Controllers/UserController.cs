using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AssignmentAPI.DTO.UserDTO;
using AssignmentAPI.Interface;
using AssignmentAPI.Models;
using AssignmentAPI.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace AssignmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _UserRepository;

        public UserController(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel<IEnumerable<UserModel>>>> GetUsers()
        {
            return await _UserRepository.GetUsersAsync();
        }

        [HttpGet("GetUserByID")]
        public async Task<ActionResult<UserModel>> GetUserByID(IDModel iDModel)
        {
            return await _UserRepository.GetUserByIdAsync(iDModel.Id);
        }


        [HttpPost]
        public async Task<ActionResult<ResponseModel<UserModel>>> PostUser(CreateUserDTO UserDTO)
        {
            return await _UserRepository.CreateUserAsync(UserDTO);
        }

        [HttpPost("Userlogin")]
        public async Task<ResponseModel<TokenResponseModel>> Userlogin(LoginUserDTO loginUserDTO)
        {
            return await _UserRepository.UserLoginChecker(loginUserDTO);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseModel<UserModel>>> PutUser(UpdateUserDTO updateUserDTO)
        {
            return await _UserRepository.UpdateUserAsync( updateUserDTO.UserId, updateUserDTO);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(IDModel iDModel)
        {
            return await _UserRepository.DeleteUserAsync(iDModel.Id);
        }
    }
}
