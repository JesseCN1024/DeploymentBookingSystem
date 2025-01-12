using DbsBookingManagementService.Models.DTOs;
using FluentValidation;
using System.Globalization;

namespace DbsBookingManagementService.Presentation.Validation
{
    public class ValidatorCreatingBookingRequestDto : AbstractValidator<CreatingBookingRequestDto>
    {
        public ValidatorCreatingBookingRequestDto()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId cannot be null.");
            RuleFor(x => x.EnvironmentId).NotEmpty().WithMessage("EnvId cannot be null.");
            //RuleFor(x => x.StartDateTime).NotEmpty().WithMessage("Ucannot be null.");
            //RuleFor(x => x.EndDateTime).NotEmpty().WithMessage("UserId cannot be null.");
            RuleFor(x => x.Notes).MaximumLength(1000);
            RuleFor(x => x.StartDateTime.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture))
                .Matches(@"\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}")
                .WithMessage("StartDateTime must be in the format: yyyy-MM-ddTHH:mm:ss");

            RuleFor(x => x.EndDateTime.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture))
                .Matches(@"\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}")
                .WithMessage("EndDateTime must be in the format: yyyy-MM-ddTHH:mm:ss");
            RuleFor(x => x)
                .Must(x => x.StartDateTime < x.EndDateTime)
                .WithMessage("StartDateTime must be less than EndDateTime");

            RuleFor(x => x)
                .Must(x => x.StartDateTime.Date == x.EndDateTime.Date)
                .WithMessage("StartDateTime and EndDateTime must be on the same day");

        }
    }
}
