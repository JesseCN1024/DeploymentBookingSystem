namespace DbsUsersManagementService.Models.DTOs
{
    public class UserSearchResponseDto
    {
        public List<UserDto> Data { get; set; } = new();
        public PaginationInfoDto Pagination { get; set; } = new();
    }

    public class PaginationInfoDto
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
    }
}
