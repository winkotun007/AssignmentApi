using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using AssignmentAPI.DTO.UserDTO;
using AssignmentAPI.Interface;
using AssignmentAPI.Models;
using AssignmentAPI.Shared;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AssignmentAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AssignmentDBContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserRepository(AssignmentDBContext context, IMapper mapper,IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<ResponseModel<IEnumerable<UserModel>>> GetUsersAsync()
        {
            ResponseModel<IEnumerable<UserModel>> responseModel = new ResponseModel<IEnumerable<UserModel>>();

            try
            {
                List<UserModel> getUsers = await _context.Users.ToListAsync();

                if (getUsers.Count == 0)
                {
                    responseModel.Code = (int)HttpStatusCode.NoContent;
                    responseModel.Data = null;
                }
                else
                {
                    responseModel.Code = (int)HttpStatusCode.OK;
                    responseModel.Data = getUsers;
                }
            }
            catch (Exception)
            {
                responseModel.Code = (int)HttpStatusCode.InternalServerError;
            }

            return responseModel;
        }

        public async Task<ResponseModel<TokenResponseModel>> UserLoginChecker(LoginUserDTO loginUserDTO)
        {
            ResponseModel<TokenResponseModel> responseModel = new ResponseModel<TokenResponseModel>();
            try
            {

                UserModel result = await _context.Users.Where(x => x.UserName == loginUserDTO.UserName).FirstOrDefaultAsync();

                if (result == null)
                {
                    responseModel.Code = (int)HttpStatusCode.NotFound;
                    responseModel.Message = ResponseMessage.UserNotFound;
                }
                else
                {
                    string oldhash = result.Password;
                    string oldsalt = result.PasswordSalt;

                    if (SaltedHash.Verify(oldsalt, oldhash, loginUserDTO.Password))
                    {
                        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                        List<Claim> claims = new List<Claim>
                        {
                            new Claim("UserId",result.UserId.ToString()),
                        };

                        var Sectoken = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                          _configuration["Jwt:Issuer"],
                          claims,
                          expires: DateTime.Now.AddMinutes(120),
                          signingCredentials: credentials);

                        string token = new JwtSecurityTokenHandler().WriteToken(Sectoken).ToString();

                        TokenResponseModel tokenResponseModel = new TokenResponseModel();
                        tokenResponseModel.Token = token;
                        tokenResponseModel.UserId = result.UserId;
                        tokenResponseModel.UserName = result.UserName;

                        responseModel.Code = (int)HttpStatusCode.OK;
                        responseModel.Message = token;
                        responseModel.Data = tokenResponseModel;
                    }

                    else
                    {
                        responseModel.Code = (int)HttpStatusCode.NotFound;
                        responseModel.Message = ResponseMessage.WrongPassword;
                    }
                }
            }
            catch (Exception)
            {
                responseModel.Code = (int)HttpStatusCode.InternalServerError;
                responseModel.Message = ResponseMessage.InternalServerError;
            }
            return responseModel;
        }

        public async Task<UserModel> GetUserByIdAsync(string id)
        {
            return await _context.Users.Where(ex => ex.UserId == id).FirstOrDefaultAsync();
        }

        public async Task<ResponseModel<UserModel>> CreateUserAsync(CreateUserDTO UserDTO)
        {
            ResponseModel<UserModel> responseModel = new ResponseModel<UserModel>();

            try
            {
                UserModel user = _mapper.Map<UserModel>(UserDTO);

                user.PasswordSalt = SaltedHash.GenerateSalt();
                user.Password = SaltedHash.ComputeHash(user.PasswordSalt,UserDTO.Password);

                _context.Users.Add(user);

                await _context.SaveChangesAsync();

                responseModel.Code = (int)HttpStatusCode.Created;
                responseModel.Message = ResponseMessage.SaveSuccessful;
                responseModel.Data = user;
            }
            catch (Exception)
            {
                responseModel.Code = (int)HttpStatusCode.InternalServerError;
                responseModel.Message = ResponseMessage.InternalServerError;
            }

            return responseModel;
        }

        public async Task<ResponseModel<UserModel>> UpdateUserAsync(string id, UpdateUserDTO updateUserDTO)
        {
            ResponseModel<UserModel> responseModel = new ResponseModel<UserModel>();

            if (id != updateUserDTO.UserId)
            {
                responseModel.Code = (int)HttpStatusCode.BadRequest;
                responseModel.Message = ResponseMessage.InternalServerError;
                return responseModel;
            }

            try
            {
                UserModel existingUser = await _context.Users.FindAsync(id);

                if (existingUser == null)
                {
                    responseModel.Code = (int)HttpStatusCode.NotFound;
                    responseModel.Message = ResponseMessage.NotFound;
                    return responseModel;
                }

                _mapper.Map(updateUserDTO, existingUser);
                existingUser.PasswordSalt = SaltedHash.GenerateSalt();
                existingUser.Password = SaltedHash.ComputeHash(existingUser.PasswordSalt,existingUser.Password);
                _context.Entry(existingUser).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                responseModel.Code = (int)HttpStatusCode.OK;
                responseModel.Message = ResponseMessage.UpdateSuccessful;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    responseModel.Code = (int)HttpStatusCode.NotFound;
                    responseModel.Message = ResponseMessage.NotFound;
                }
                else
                {
                    responseModel.Code = (int)HttpStatusCode.InternalServerError;
                    responseModel.Message = ResponseMessage.InternalServerError;
                }
            }

            return responseModel;
        }

        public async Task<IActionResult> DeleteUserAsync(string id)
        {
            var User = await _context.Users.FindAsync(id);
            if (User == null)
            {
                return new NotFoundResult();
            }

            _context.Users.Remove(User);
            await _context.SaveChangesAsync();

            return new NoContentResult();
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }

    }
}

