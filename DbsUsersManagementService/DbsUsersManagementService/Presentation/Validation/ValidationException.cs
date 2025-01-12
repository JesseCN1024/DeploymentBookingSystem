using DbsUsersManagementService.Models.ErrorModels;
using System.Diagnostics.CodeAnalysis;

namespace DbsUsersManagementService.Presentation.Validation
{
    [ExcludeFromCodeCoverage]
    public class ValidationException : Exception
    {
        public ErrorResponse ErrorResponse { get; private set; }

        public ValidationException(ErrorResponse errorResponse)
        {
            this.ErrorResponse = errorResponse;
        }
    }
}
