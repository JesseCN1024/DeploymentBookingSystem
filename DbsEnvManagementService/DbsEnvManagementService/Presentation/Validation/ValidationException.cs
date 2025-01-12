using DbsEnvManagementService.Models.ErrorModels;
using System.Diagnostics.CodeAnalysis;

namespace DbsEnvManagementService.Presentation.Validation
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
