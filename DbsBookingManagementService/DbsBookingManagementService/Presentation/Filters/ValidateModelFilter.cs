using DbsBookingManagementService.Models.ErrorModels;
using DbsBookingManagementService.Presentation.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.CodeAnalysis;

namespace DbsBookingManagementService.Presentation.Filters
{
    [ExcludeFromCodeCoverage]
    public class ValidateModelFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var fluent = DbsBookingManagementService.Presentation.Constants.Constants.Validation.FluentValidationErrorKey;
            if (!context.ModelState.IsValid)
            {
                if (context.ModelState.ContainsKey(fluent))
                {
                    context.Result = new UnprocessableEntityObjectResult(((ValidationException)context
                        .ModelState[fluent].Errors[0].Exception).ErrorResponse);
                }
                else
                {
                    var errors = context.ModelState.Keys
                        .SelectMany(key => context.ModelState[key].Errors
                            .Select(x => new Error($"user_service.invalid_value",
                                $"{x.ErrorMessage}({key})")))
                        .ToList();

                    context.Result = new BadRequestObjectResult(new ErrorResponse { Errors = errors });
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}