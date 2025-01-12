using DbsEnvManagementService.Models.DTOs;
using FluentValidation;

namespace DbsEnvManagementService.Presentation.Validation
{
    public class ValidatorSearchRequestDto : AbstractValidator<SearchRequestDto>
    {
        public ValidatorSearchRequestDto()
        {
            RuleFor(x => x.searchKey).NotNull().WithMessage("Search key cannot be null.");
            RuleFor(x => x.Pagination).SetValidator(new PaginationDtoValidator());
            RuleForEach(x => x.Sorts).SetValidator(new SortOptionDtoValidator());
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
                .Must(name => name == "createdDate" || name == "name")
                .WithMessage("Sort option name must be either 'createdDate' or 'name'.");
            RuleFor(x => x.Direction)
                .Matches("Ascending|Descending")
                .WithMessage("Sort direction must be Ascending or Descending.");
        }
    }
}
