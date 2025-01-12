namespace DbsBookingManagementService.Models.DTOs
{
    public class CreatingBookingRequestDto
    {
        public Guid UserId { get; set; }
        public Guid EnvironmentId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Notes { get; set; } = "";
        //public CreatingBookingRequestDto()
        //{
        //    StartDateTime = DateTime.SpecifyKind(StartDateTime, DateTimeKind.Utc);
        //    EndDateTime = DateTime.SpecifyKind(EndDateTime, DateTimeKind.Utc);
        //}
    }
}
