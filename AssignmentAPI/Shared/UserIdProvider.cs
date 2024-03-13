using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace AssignmentAPI.Shared
{
    public class UserIdProvider : IUserIdProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        public UserIdProvider(IHttpContextAccessor httpContextAccessor,IConfiguration configuration)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }



        public string? GetUserId()
        {
            var bearerToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var JwtIssuer = _configuration["Jwt:Issuer"];

            try
            {
                tokenHandler.ValidateToken(bearerToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = JwtIssuer,
                    ValidAudience = JwtIssuer,
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                // Retrieve UserId from the token's claims
                var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserId");

                if (userIdClaim != null)
                {
                    return userIdClaim.ToString();
                }
                else
                {
                    // Handle the case where UserId claim is missing or not a valid integer
                    return null;
                }
            }
            catch (SecurityTokenValidationException)
            {
                // Handle validation exception
                return null;
            }
            catch (Exception)
            {
                // Handle other exceptions
                return null;
            }
        }
    }
}

