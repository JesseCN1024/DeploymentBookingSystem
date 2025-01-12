namespace DbsEnvManagementService.Models.DTOs
{
    public class EnvResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Account { get; set; }
        public string Stack { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; } // Available, Booked, Broken
    }
}
