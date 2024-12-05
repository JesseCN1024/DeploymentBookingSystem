using DbsUsersManagementService.Models.DTOs;
using FluentValidation;

namespace DbsUsersManagementService.Validators
{
    public class RegisterRequestDtoValidator : AbstractValidator<RegisterRequestDto>   
    {
        public RegisterRequestDtoValidator()
        {
            // Username validation 
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username is required.")
                .Length(3, 50).WithMessage("Username must be between 3 and 50 characters");
 
            RuleFor(x => x.email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            // TeamId validation
            RuleFor(x => x.teamId)
                .NotEmpty().WithMessage("TeamId is required.")
                .Must(id => id != Guid.Empty).WithMessage("Invalid TeamId.");

            // RoleId validation
            RuleFor(x => x.roleId)
                .NotEmpty().WithMessage("RoleId is required.")
                .Must(id => id != Guid.Empty).WithMessage("Invalid RoleId.");
        }
    }
}
