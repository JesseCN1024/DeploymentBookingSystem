using FluentValidation;

namespace DbsBookingManagementService.Utilities
{
    public static class ValidationHelper
    {
        public static void ValidateRequest<T>(IValidator<T> validator, T request)
        {
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
        }
    }
}
