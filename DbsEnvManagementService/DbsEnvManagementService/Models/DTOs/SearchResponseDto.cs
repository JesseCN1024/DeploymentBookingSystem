namespace DbsEnvManagementService.Models.DTOs
{
    public class SearchResponseDto
    {
        public List<EnvResponseDto> Data { get; set; } = new();
        public PaginationInfoDto Pagination { get; set; } = new();
    }

    public class PaginationInfoDto
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int? Count { get; set; }
    }
}
