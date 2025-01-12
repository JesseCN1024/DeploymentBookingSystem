using DbsUsersManagementService.Models.DTOs;
using FluentValidation;

namespace DbsUsersManagementService.Presentation.Validation
{
    public class ValidatorUpdateRequestDto :  AbstractValidator<UpdateRequestDto>
    {
        public ValidatorUpdateRequestDto()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.")
                .Length(3, 50).WithMessage("Username must be between 3 and 50 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            // TeamId validation
            RuleFor(x => x.TeamId)
                .NotEmpty().WithMessage("TeamId is required.")
                .Must(id => id != Guid.Empty).WithMessage("Invalid TeamId.");

            // RoleId validation
            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage("RoleId is required.")
                .Must(id => id != Guid.Empty).WithMessage("Invalid RoleId.");

            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("IsActive is required.");

        }
    }
}
