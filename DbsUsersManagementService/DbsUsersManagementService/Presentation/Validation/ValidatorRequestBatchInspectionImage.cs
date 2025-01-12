using DbsUsersManagementService.Models.DTOs;
using FluentValidation;

namespace DbsUsersManagementService.Presentation.Validation
{
    public class ValidatorRequestBatchInspectionImage : AbstractValidator<RequestBatchInspectionImageUploadUrlModel>
    {
        public ValidatorRequestBatchInspectionImage()
        {
            RuleFor(x => x.InspectionId).NotEmpty().WithMessage("InspectionId is required");
            RuleFor(x => x.ImageName)
                .NotEmpty().WithMessage("ImageName is required")
                .Length(3, 50).WithMessage("ImageName must be between 3 and 50 characters");

        }

    }
}
