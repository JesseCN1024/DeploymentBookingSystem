using DbsEnvManagementService.Models.DTOs;
using DbsEnvManagementService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DbsEnvManagementService.Controllers
{
    [Route("environments")]
    [ApiController]
    public class EnvController : ControllerBase
    {
        private IEnvService _envService;

        public EnvController(IEnvService envService)
        {
            _envService = envService;

        }

        [HttpGet("{environmentId}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetEnvById([FromRoute] Guid environmentId)
        {

            var env = await _envService.GetEnvByIdAsync(environmentId);
            return Ok(env);

        }

        [HttpGet]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetAllEnvs([FromBody] SearchRequestDto requestDto) 
        {

            var user = await _envService.GetAllEnvsAsync(requestDto);
            return Ok(user);
        }




        [HttpPost]
        [Authorize(Policy="AdminOnly")]
        public async Task<IActionResult> CreateEnv([FromBody] UpdateRequestDto requestDto)
        {
            var envId = await _envService.CreateEnvAsync(requestDto);
            if (envId == Guid.Empty)
            {
                return BadRequest();
            }
            var locationUrl = Url.Action(nameof(GetEnvById), new { environmentId = envId });
            return Created(locationUrl, new { Message = "Environment registered successfully" });


        }


        [HttpPut("{environmentId}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdateEnv([FromRoute] Guid environmentId, [FromBody] UpdateRequestDto requestDto)
        {
            await _envService.UpdateEnvAsync(environmentId, requestDto);
            return Ok(new { Message = "Environment updated successfully" });
        }



        [HttpDelete("{environmentId}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteEnv([FromRoute] Guid environmentId)
        {
            try
            {
                await _envService.DeleteEnvAsync(environmentId);
                return Ok(new { Message = "Env deleted successfully!" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }   







    }
}
