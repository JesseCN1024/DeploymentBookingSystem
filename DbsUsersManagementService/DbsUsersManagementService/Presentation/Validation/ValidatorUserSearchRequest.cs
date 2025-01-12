using DbsUsersManagementService.Models.DTOs;
using FluentValidation;

namespace DbsUsersManagementService.Presentation.Validation
{
    public class UserSearchRequestDtoValidator : AbstractValidator<UserSearchRequestDto>
    {
        public UserSearchRequestDtoValidator()
        {
            RuleFor(x => x.SearchFields).SetValidator(new SearchFieldsDtoValidator());
            RuleFor(x => x.Pagination).SetValidator(new PaginationDtoValidator());
            RuleForEach(x => x.Sorts).SetValidator(new SortOptionDtoValidator());
        }
    }

    public class SearchFieldsDtoValidator : AbstractValidator<SearchFieldsDto>
    {
        public SearchFieldsDtoValidator()
        {
            RuleFor(x => x.UserIds).NotEmpty().WithMessage("UserIds must contain at least one element.");
            RuleFor(x => x.TeamIds).NotEmpty().WithMessage("TeamIds must contain at least one element.");
            RuleFor(x => x.RoleIds).NotEmpty().WithMessage("RoleIds must contain at least one element.");
            RuleFor(x => x.UserNames).NotEmpty().WithMessage("RoleIds must contain at least one element.");
        }
    }

    public class PaginationDtoValidator : AbstractValidator<PaginationDto>
    {
        public PaginationDtoValidator()
        {
            RuleFor(x => x.Page).GreaterThan(0).WithMessage("Page must be greater than 0.");
            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("PageSize must be greater than 0.");
        }
    }

    public class SortOptionDtoValidator : AbstractValidator<SortOptionDto>
    {
        public SortOptionDtoValidator()
        {
            RuleFor(x => x.Name)
                .Must(name => name == "createdDate" || name == "username")
                .WithMessage("Sort option name must be either 'createdDate' or 'username'.");
            RuleFor(x => x.Direction)
                .Matches("Ascending|Descending")
                .WithMessage("Sort direction must be Ascending or Descending.");
        }
    }
}
