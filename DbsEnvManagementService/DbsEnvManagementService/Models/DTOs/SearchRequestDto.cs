namespace DbsEnvManagementService.Models.DTOs
{
    public class SearchRequestDto
    {
        public string searchKey { get; set; } = "" ;
        public PaginationDto Pagination { get; set; } = new();
        public List<SortOptionDto> Sorts { get; set; } = new();
    }
    public class PaginationDto
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 25;
    }

    public class SortOptionDto
    {
        public string Name { get; set; }

        public string Direction { get; set; }
    }
}
