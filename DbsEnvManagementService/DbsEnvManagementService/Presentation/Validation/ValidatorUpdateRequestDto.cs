using DbsEnvManagementService.Models.DTOs;
using FluentValidation;

namespace DbsEnvManagementService.Presentation.Validation
{
    public class ValidatorUpdateRequestDto : AbstractValidator<UpdateRequestDto>
    {
        public ValidatorUpdateRequestDto()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.DisplayName).NotEmpty().WithMessage("DisplayName is required");
            RuleFor(x => x.Account).NotEmpty().WithMessage("Account is required");
            RuleFor(x => x.Stack).NotEmpty().WithMessage("Stack is required");
            RuleFor(x => x.Notes).NotEmpty().WithMessage("Notes is required");
            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required")
                .Must(status => new[] { "Available", "Booked", "Broken" }.Contains(status))
                .WithMessage("Status must be one of the following values: Available, Booked, Broken");
        }
    }
}
