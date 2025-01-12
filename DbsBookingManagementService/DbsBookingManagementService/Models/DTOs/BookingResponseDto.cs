namespace DbsBookingManagementService.Models.DTOs
{
    public class BookingResponseDto
    {
        public Guid BookingId { get; set; }
        public Guid UserId { get; set; }
        public Guid EnvironmentId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Notes { get; set; } = "";
    }
}
