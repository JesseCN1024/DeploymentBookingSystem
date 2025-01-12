using DbsBFF.ApplicationLogic.Implementations;
using DbsBFF.ApplicationLogic.Inferfaces;
using DbsBFF.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DbsBFF.Controllers
{
    [ApiController]
    [Route("deployment-app-bff")]
    public class AppController : ControllerBase
    {
        private IDeploymentSolver _deploymentSolver;
        private IHttpContextAccessor _httpContextAccessor;

        public AppController(IDeploymentSolver deploymentSolver, IHttpContextAccessor httpContextAccessor)
        {
            _deploymentSolver = deploymentSolver;
            _httpContextAccessor = httpContextAccessor;
        }


        [HttpGet("booking/{bookingId}")]
        public async Task<IActionResult> GetBookingId([FromRoute] Guid bookingId)
        {
            return Ok(200);
        }

        [HttpPost("users/login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            try
            {
                var response  = await _deploymentSolver.LoginAsync(request);
                return Ok(response);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        // Booking Section
        [HttpPost("booking")]
        [Authorize(Policy = "AllUsers")]
        public async Task<IActionResult> CreateBooking([FromBody] CreatingBookingRequestDto request)
        {
            var bookingId = await _deploymentSolver.CreateBookingAsync(request);
            if (bookingId == Guid.Empty)
            {
                return BadRequest();
            }
            var locationUrl = Url.Action(nameof(GetBookingId), new { bookingId });
            return Created(locationUrl, new { Message = "Booking created successfully" });
        }



        [HttpGet("booking")]
        [Authorize(Policy = "AllUsers")]
        public async Task<IActionResult> GetAllBooking([FromQuery] Guid? userId,
            [FromQuery] Guid? environmentId, [FromQuery] DateTime? fromDate,
            [FromQuery] DateTime? toDate)
        {
            var bookings = await _deploymentSolver.GetAllBookingsAsync(userId, environmentId, fromDate, toDate);
            return Ok(bookings);
        }


    }
}
