namespace DbsBookingManagementService.Models.DTOs
{
    public class EditBookingRequestDto
    {
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Notes { get; set; } = "";
    }
}
