using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using FluentValidation;
using FluentValidation.Results;
using DbsEnvManagementService.Models.ErrorModels;

namespace DbsEnvManagementService.Presentation.Validation
{
    [ExcludeFromCodeCoverage]
    public class ValidatorInterceptor : IValidatorInterceptor
    {
        public IValidationContext BeforeAspNetValidation(ActionContext actionContext, IValidationContext commonContext)
        {
            return commonContext;
        }
        public ValidationResult AfterAspNetValidation(ActionContext actionContext, IValidationContext validationContext, ValidationResult result)
        {
            if (!result.IsValid)
            {
                actionContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                List<Error> errors = [];
                foreach (var error in result.Errors)
                {
                    var validationError = new Error($"{Constants.Application.Name}.{error.ErrorCode}", error.ErrorMessage);
                    validationError.AddErrorProperty(new ErrorProperty(
                        error.PropertyName,
                        error.AttemptedValue?.ToString() ?? "null"));

                    errors.Add(validationError);
                }
                var errorResponse = new ErrorResponse(errors);
                actionContext.ModelState.TryAddModelException(Constants.Constants.Validation.FluentValidationErrorKey,
                    new ValidationException(errorResponse));
            }

            return result;
        }
    }
}
