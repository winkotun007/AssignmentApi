using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using AssignmentAPI.Interface;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace AssignmentAPI.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CustomMiddleware> _logger;
        public CustomMiddleware(RequestDelegate next, JsonSerializerSettings serializerSettings,IConfiguration configuration,ILogger<CustomMiddleware> logger)
        {
            _next = next;
            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
            _configuration = configuration;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext,IGuestAcessRepository guestAcessRepository)
        {
            try
            {

                if (httpContext.Request.Method == "OPTIONS")
                {
                    await _next(httpContext);
                    return;
                }

                bool isTrueAccess = false;

                if (!string.IsNullOrEmpty(httpContext.Request.Method))
                {
                   isTrueAccess= guestAcessRepository.isAccessByPath(httpContext.Request.Path, httpContext.Request.Method).Result;
                }


                if (isTrueAccess)
                {
                    await _next(httpContext);
                    return;
                    //await ResponseMessage(new { status = "fail", data = "Method Not Allowed" }, httpContext, StatusCodes.Status405MethodNotAllowed);
                    //return;
                }


                /// Validate Bearer Token
                var bearerToken = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                if (string.IsNullOrEmpty(bearerToken) || !IsValidBearerToken(bearerToken))
                {
                    await ResponseMessage(new { status = "fail", data = "Token is Something Wrong." }, httpContext, StatusCodes.Status405MethodNotAllowed);
                    return;
                }
    
                await _next(httpContext);
                return;

            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message.ToString());
                string exmsg = ex.Message.ToString();
                await ResponseMessage(new { status = "fail", data = "Method Not Allowed" }, httpContext, StatusCodes.Status405MethodNotAllowed);
            }
           await _next(httpContext);
            return;
        }

        public async Task ResponseMessage(object data, HttpContext context, int code = StatusCodes.Status400BadRequest)
        {
            context.Response.StatusCode = code;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(data, _serializerSettings));
        }

        /// <summary>
        /// Token Check 
        private bool IsValidBearerToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var JwtIssuer = _configuration["Jwt:Issuer"];
            var Jwtkey = _configuration["Jwt:Key"];

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
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

                return true;
            }
            catch (SecurityTokenValidationException ex)
            {
                var exmee = ex.Message.ToString();
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddlewareClassTemplate(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomMiddleware>();
        }
    }
}

