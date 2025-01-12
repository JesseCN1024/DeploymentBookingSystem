using DbsEnvManagementService.Mappings;
using DbsEnvManagementService.Presentation.Filters;
using DbsEnvManagementService.Presentation.Validation;
using DbsEnvManagementService.Startup;
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
using DbsEnvManagementService.Presentation.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using DbsBFF.Presentation.Validation.Validators;
using DbsBFF.Services.Interfaces;
using DbsBFF.Services;
using DbsBFF.ApplicationLogic.Inferfaces;
using DbsBFF.ApplicationLogic.Implementations;
using DbsBFF.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);


// CONFIGURATION ------------------------------------

// Configure environment-specific settings
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Logging
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/NzWalks_Log.txt", rollingInterval: RollingInterval.Minute)
    .MinimumLevel.Warning() // Information() ,  Warning(), .... Hierachy
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
// Model Filter
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
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidateModelFilter>();
});

builder.Services.AddLogging(config =>
{
    config.ClearProviders();
    config.AddConsole();
    config.AddDebug();
});



builder.Services.AddFluentValidation(fv => {
    fv.RegisterValidatorsFromAssemblyContaining<ValidatorLoginRequestDto>();
    fv.RegisterValidatorsFromAssemblyContaining<ValidatorCreatingBookingRequestDto>();
});



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
// HTTP Client
builder.Services.AddHttpClient<IUserService, UserService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:UserService:BaseUrl"]!);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});


builder.Services.AddSingleton<IAuthorizationHandler, CustomAuthorizationHandler>();
builder.Services.AddTransient<IValidatorInterceptor, ValidatorInterceptor>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IEnvironmentService, EnvironmentService>();
builder.Services.AddScoped<IDeploymentSolver, DeploymentSolver>();
builder.Services.AddSingleton<IAuthorizationHandler, CustomAuthorizationHandler>();
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

app.UseRouting();
app.UseAuthorization();




app.MapControllers();



app.Run();
