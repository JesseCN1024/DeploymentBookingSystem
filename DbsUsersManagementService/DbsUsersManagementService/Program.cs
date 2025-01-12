using DbsUsersManagementService.Data;
using DbsUsersManagementService.Mappings;
using DbsUsersManagementService.Models.DTOs;
using DbsUsersManagementService.Presentation.Filters;
using DbsUsersManagementService.Presentation.Validation;
using DbsUsersManagementService.Repositories;
using DbsUsersManagementService.Services.Implementations;
using DbsUsersManagementService.Services.Interfaces;
using DbsUsersManagementService.Startup;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using AutoMapper;
using DbsUsersManagementService.Presentation.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


// CONFIGURATION ------------------------------------


var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/NzWalks_Log.txt", rollingInterval: RollingInterval.Minute)
    .MinimumLevel.Warning() // Information() ,  Warning(), .... Hierachy
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);



builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});


// Auto Mapper Configurations
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AutoMapperProfiles());
});









// DI INJECTION  and ADDING ------------------------------------

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidateModelFilter>();
});

builder.Services.AddLogging(config =>
{
    config.ClearProviders();
    config.AddConsole();
    config.AddDebug();
    // Add other logging providers as needed
});



builder.Services.AddFluentValidation(fv => {
    // Register all validators from this assembly
    fv.RegisterValidatorsFromAssemblyContaining<ValidatorRequestBatchInspectionImage>();
    fv.RegisterValidatorsFromAssemblyContaining<ValidatorRegisterRequestDto>();
    fv.RegisterValidatorsFromAssemblyContaining<ValidatorUpdateRequestDto>();
    fv.RegisterValidatorsFromAssemblyContaining<ValidatorLoginRequestDto>();
});

builder.Services.AddValidatorsFromAssemblyContaining<UserSearchRequestDtoValidator>();


// Add services to the container.
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Please enter JWT with Bearer into field"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


// DB Context



builder.Services.AddDbContext<DbsUserAuthDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DbsConnectionString")));
// Register services
builder.Services.AddScoped<IValidator<RegisterRequestDto>, ValidatorRegisterRequestDto>();
builder.Services.AddScoped<IPasswordHasher<String>, PasswordHasher<String>>();
//builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IAuthenService, AuthenService>();

builder.Services.AddSingleton<IAuthorizationHandler, CustomAuthorizationHandler>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddTransient<IValidatorInterceptor, ValidatorInterceptor>();



// JWT Authentication

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var configuration = builder.Configuration;
    var key = Encoding.ASCII.GetBytes(configuration["Jwt:Secret"]);
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});




IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddMvc();





builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AllUsers", policy =>
        policy.Requirements.Add(new CustomAuthorizationRequirement(new[] { "ADMIN", "POWER_USER", "GENERAL_USER" })));
    options.AddPolicy("AdminOnly", policy =>
        policy.Requirements.Add(new CustomAuthorizationRequirement(new[] { "ADMIN" })));

    options.AddPolicy("AdminOrPowerUser", policy =>
        policy.Requirements.Add(new CustomAuthorizationRequirement(new[] { "ADMIN", "POWER_USER" })));

});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();


var app = builder.Build();







// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

//app.MapIdentityApi<User>();

app.UseHttpsRedirection();

app.UseAuthentication();


app.ConfigureExceptionHandler(app.Services.GetService<ILoggerFactory>()!.CreateLogger("Exceptions"));


app.UseAuthorization();


app.MapControllers();

app.Run();
