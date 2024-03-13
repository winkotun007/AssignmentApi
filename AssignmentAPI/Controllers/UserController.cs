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

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUser(string id)
        {
            return await _UserRepository.GetUserByIdAsync(id);
        }


        [HttpPost]
        public async Task<ActionResult<ResponseModel<UserModel>>> PostUser(CreateUserDTO UserDTO)
        {
            return await _UserRepository.CreateUserAsync(UserDTO);
        }

        [Route("Userlogin")]
        [HttpPost]
        public async Task<ResponseModel<TokenResponseModel>> Userlogin(LoginUserDTO loginUserDTO)
        {
            return await _UserRepository.UserLoginChecker(loginUserDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseModel<UserModel>>> PutUser(string id, UpdateUserDTO updateUserDTO)
        {
            return await _UserRepository.UpdateUserAsync(id, updateUserDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            return await _UserRepository.DeleteUserAsync(id);
        }
    }
}
