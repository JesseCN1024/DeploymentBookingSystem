using System.ComponentModel.DataAnnotations;

namespace DbsUsersManagementService.Models.DTOs
{
    public class UserSearchRequestDto
    {
        public SearchFieldsDto SearchFields { get; set; } = new();
        public PaginationDto Pagination { get; set; } = new();
        public List<SortOptionDto> Sorts { get; set; } = new();
    }

    public class SearchFieldsDto
    {
        public List<Guid> UserIds { get; set; }

        public List<Guid> TeamIds { get; set; }

        public List<Guid> RoleIds { get; set; }

        public List<string> UserNames { get; set; } = new();
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
