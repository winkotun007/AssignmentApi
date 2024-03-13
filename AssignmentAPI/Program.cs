using Microsoft.Extensions.Configuration;
using AssignmentAPI.Shared;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using AssignmentAPI.Middleware;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var serverVersion = new MySqlServerVersion(new Version(8, 0, 35));
// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
//8.0.35
builder.Services.AddAutoMapper(typeof(Program));


builder.Services.AddCors(options =>
{
    options.AddPolicy("*", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

//Jwt configuration starts here
var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = jwtIssuer,
         ValidAudience = jwtIssuer,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
     };
 });
//Jwt configuration ends here

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<Newtonsoft.Json.JsonSerializerSettings>(new Newtonsoft.Json.JsonSerializerSettings
{
    // Customize JsonSerializerSettings as needed
});

builder.Services.AddBuildingRepository();

builder.Services.AddDbContext<AssignmentDBContext>(option=>option.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), serverVersion));


var app = builder.Build();

app.UseMiddleware<CustomMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseCors();            
app.UseCors("*");
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

