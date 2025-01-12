using DbsBookingManagementService.Models.DTOs;
using DbsBookingManagementService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DbsBookingManagementService.Controllers
{
    [ApiController]
    [Route("booking")]
    public class BookingController : ControllerBase
    {
        private IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;

            
        }

        [HttpGet("{bookingId}")]
        [Authorize(Policy = "AllUsers")]
        public async Task<IActionResult> GetBookingById(Guid bookingId)
        {
            var bookingDto = await _bookingService.GetBookingByIdAsync(bookingId);
            return Ok(bookingDto);
        }


        [HttpGet]
        [Authorize(Policy = "AllUsers")]
        public async Task<IActionResult> GetAll([FromQuery] Guid? userId, 
            [FromQuery] Guid? environmentId, [FromQuery] DateTime? fromDate, 
            [FromQuery] DateTime? toDate)
        {
            var bookings = await _bookingService.GetAllBookingsAsync(userId, environmentId, fromDate, toDate);
            return Ok(bookings);
        }


        [HttpPost]
        [Authorize(Policy = "AllUsers")]
        public async Task<IActionResult> CreateBooking([FromBody] CreatingBookingRequestDto requestDto)
        {
            var bookingId = await _bookingService.CreateBookingAsync(requestDto);
            if (bookingId == Guid.Empty)
            {
                return BadRequest();
            }
            var locationUrl =  Url.Action(nameof(GetBookingById), new { bookingId });
            return Created(locationUrl, new { Message = "Booking created successfully" });
        }


        [HttpPost("{bookingId}")]
        [Authorize(Policy = "AllUsers")]
        public async Task<IActionResult> UpdateBooking([FromRoute] Guid bookingId, [FromBody] EditBookingRequestDto requestDto )
        {
            var bookingDto = await _bookingService.UpdateBookingAsync(bookingId, requestDto);
            return Ok(bookingDto);
             
        }


        [HttpDelete("{bookingId}")]
        [Authorize(Policy = "AdminOrGeneral")]
        public async Task<IActionResult> DeleteBooking([FromRoute] Guid bookingId)
        {
            await _bookingService.DeleteBookingAsync(bookingId);
            return Ok(new { Message = "User deleted successfully!" });
        }










    }
}
