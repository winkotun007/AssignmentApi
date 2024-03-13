using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssignmentAPI.DTO.UserDTO;
using AssignmentAPI.Models;
using AssignmentAPI.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentAPI.Interface
{
	public interface IUserRepository
	{
        Task<ResponseModel<IEnumerable<UserModel>>> GetUsersAsync();
        Task<UserModel> GetUserByIdAsync(string id);
        Task<ResponseModel<UserModel>> CreateUserAsync(CreateUserDTO userDTO);
        Task<ResponseModel<UserModel>> UpdateUserAsync(string id, UpdateUserDTO updateUserDTO);
        Task<IActionResult> DeleteUserAsync(string id);
        Task<ResponseModel<TokenResponseModel>> UserLoginChecker(LoginUserDTO loginUserDTO);
    }
}

