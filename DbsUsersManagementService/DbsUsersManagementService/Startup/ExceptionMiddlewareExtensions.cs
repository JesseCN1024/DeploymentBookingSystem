﻿using DbsUsersManagementService.Utilities;
using Microsoft.AspNetCore.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using DbsUsersManagementService.Presentation.Constants;
using Application = DbsUsersManagementService.Presentation.Constants.Application;

namespace DbsUsersManagementService.Startup
{
    [ExcludeFromCodeCoverage]
    public static class ExceptionMiddlewareExtensions
    {
        [ExcludeFromCodeCoverage]
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = Application.Json;
                    var errorId = Guid.NewGuid();
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.LogError($"ErrorId:{errorId} Exception:{contextFeature.Error}");
                        string errorMessage = string.Empty;
                        string errorCode = string.Empty;

                        if (contextFeature.Error is UserFriendlyException)
                        {
                            var userFriendlyException = (UserFriendlyException)contextFeature.Error;
                            switch (userFriendlyException.ErrorCode)
                            {
                                case ErrorCode.NotFound:
                                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                                    errorMessage = userFriendlyException.UserFriendlyMessage;
                                    errorCode = $"{Application.Name}.not_found";
                                    break;
                                case ErrorCode.Unauthorized:
                                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                    errorMessage = userFriendlyException.UserFriendlyMessage;
                                    errorCode = $"{Application.Name}.unauthorized";
                                    break;
                                case ErrorCode.VersionConflict:
                                    context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                                    errorMessage = userFriendlyException.UserFriendlyMessage;
                                    errorCode = $"{Application.Name}.version_conflict";
                                    break;
                                case ErrorCode.ItemAlreadyExists:
                                    context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                                    errorMessage = userFriendlyException.UserFriendlyMessage;
                                    errorCode = $"{Application.Name}.item_exists";
                                    break;
                                case ErrorCode.Conflict:
                                    context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                                    errorMessage = userFriendlyException.UserFriendlyMessage;
                                    errorCode = $"{Application.Name}.conflict";
                                    break;
                                case ErrorCode.BadRequest:
                                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                                    errorMessage = userFriendlyException.UserFriendlyMessage;
                                    errorCode = $"{Application.Name}.bad_request";
                                    break;
                                case ErrorCode.UnprocessableEntity:
                                    context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                                    errorMessage = userFriendlyException.UserFriendlyMessage;
                                    errorCode = $"{Application.Name}.un_processable_entity";
                                    break;
                                case ErrorCode.Forbidden:
                                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                                    errorMessage = userFriendlyException.UserFriendlyMessage;
                                    errorCode = $"{Application.Name}.forbidden";
                                    break;
                                default:
                                    context.Response.StatusCode = 500;
                                    errorMessage = userFriendlyException.UserFriendlyMessage;
                                    errorCode = $"{Application.Name}.general_error";
                                    break;
                            }
                        }
                        else if (contextFeature.Error is ServerException serverException)
                        {
                            switch (serverException.ErrorCode)
                            {
                                case ServerErrorCode.ServiceUnavailable:
                                    context.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                                    errorMessage = serverException.Message;
                                    errorCode = $"{Application.Name}.service_unavailable";
                                    break;
                                default:
                                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                    errorMessage = serverException.Message;
                                    errorCode = $"{Application.Name}.general_error";
                                    break;
                            }
                        }
                        //else if (contextFeature.Error is ValidationException validationException)
                        //{
                        //    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        //    errorMessage = validationException.Message;
                        //    errorCode = $"{Application.Name}.validation_error";
                        //}
                        else
                        {
                            context.Response.StatusCode = 500;
                            errorCode = $"{Application.Name}.general_error";
                            errorMessage = "An error has occured.";
                        }
                        await context.Response.WriteAsync($@"
                        {{
                            ""errors"":[
                                {{
                                    ""code"":""{errorCode}"",
                                    ""message"":""{errorMessage}, ErrorId:{errorId}""
                                }}
                            ]
                        }}");
                    }
                });
            });
        }
    }
}
