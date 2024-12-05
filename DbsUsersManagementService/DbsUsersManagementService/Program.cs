using DbsUsersManagementService.Data;
using DbsUsersManagementService.Models.DTOs;
using DbsUsersManagementService.Services.Implementations;
using DbsUsersManagementService.Services.Interfaces;
using DbsUsersManagementService.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// DB Context
builder.Services.AddDbContext<DbsUserDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbsUserConnectionString")));
builder.Services.AddDbContext<DbsUserAuthDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbsUserAuthConnectionString")));
// Register services
builder.Services.AddScoped<IValidator<RegisterRequestDto>, RegisterRequestDtoValidator>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();





builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("DbsUser")
    .AddEntityFrameworkStores<DbsUserAuthDbContext>()
    .AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

// Add JWT authentication to the services collection
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    // Configure JWT Bearer options
    .AddJwtBearer(options =>
    {
        // Set token validation parameters
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Validate the issuer of the token
            ValidateIssuer = true,
            // Validate the audience of the token
            ValidateAudience = true,
            // Validate the token's lifetime
            ValidateLifetime = true,
            // Validate the signing key used to sign the token
            ValidateIssuerSigningKey = true,
            // Set the valid issuer, read from configuration
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            // Set the valid audience, read from configuration
            ValidAudience = builder.Configuration["Jwt:Audience"],
            // Set the signing key, read from configuration and encoded as a symmetric security key
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

var app = builder.Build();






// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
