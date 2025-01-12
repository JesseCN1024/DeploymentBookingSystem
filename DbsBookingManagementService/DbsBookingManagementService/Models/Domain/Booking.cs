using DbsBookingManagementService.Models.Base;

namespace DbsBookingManagementService.Models.Domain
{
    public class Booking : AuditableEntity
    {
        public Guid UserId { get; set; }
        public Guid EnvironmentId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Notes { get; set; } = "";
        //public Booking()
        //{
        //    StartDateTime = DateTime.SpecifyKind(StartDateTime, DateTimeKind.Utc);
        //    EndDateTime = DateTime.SpecifyKind(EndDateTime, DateTimeKind.Utc);
        //}

    }
}
