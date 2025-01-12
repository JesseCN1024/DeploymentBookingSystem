﻿using DbsBFF.Models.DTOs;
using FluentValidation;

namespace DbsBFF.Presentation.Validation.Validators
{
    public class ValidatorLoginRequestDto : AbstractValidator<LoginRequestDto>
    {
        public ValidatorLoginRequestDto()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .Length(3, 50).WithMessage("Username must be between 3 and 50 characters");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("Password must contain at least one number")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character");

        }
    }
}
