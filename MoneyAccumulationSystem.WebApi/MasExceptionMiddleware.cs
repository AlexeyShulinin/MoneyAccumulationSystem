using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoneyAccumulationSystem.Services.Exceptions;

namespace MoneyAccumulationSystem.WebApi;

public class MasExceptionMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<MasExceptionMiddleware> logger;
    
    public MasExceptionMiddleware(RequestDelegate next, ILogger<MasExceptionMiddleware> logger)
    {
        this.logger = logger;
        this.next = next;
    }
    
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            logger.LogError("Something went wrong: {Ex}", ex);
            await HandleExceptionAsync(httpContext, ex);
        }
    }
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        string message;
        
        switch (exception)
        {
            case ValidationException ex:
            {
                var problemDetails = new ValidationProblemDetails(ex.Errors)
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Title = "Validation fails",
                    Status = (int)HttpStatusCode.BadRequest,
                    Instance = context.Request.Path
                };

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                message = JsonSerializer.Serialize(problemDetails);
                break;
            }
            case NotFoundException:
            {
                var problemDetails = new ProblemDetails
                {
                    Type = "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.4",
                    Title = "Not found",
                    Status = (int)HttpStatusCode.NotFound,
                    Instance = context.Request.Path
                };
                
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                message = JsonSerializer.Serialize(problemDetails);
                break;
            }
            case ForbiddenException:
            {
                var problemDetails = new ProblemDetails
                {
                    Type = "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.3",
                    Title = "Forbidden",
                    Status = (int)HttpStatusCode.Forbidden,
                    Instance = context.Request.Path
                };
                
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                message = JsonSerializer.Serialize(problemDetails);
                break;
            }
            default:
            {
                var problemDetails = new ProblemDetails
                {
                    Type = "https://www.rfc-editor.org/rfc/rfc7231#section-6.6.1",
                    Title = "Internal Server Error",
                    Status = (int)HttpStatusCode.InternalServerError,
                    Instance = context.Request.Path
                };
                
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                message = JsonSerializer.Serialize(problemDetails);
                break;
            }
        }
        
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(message);
    }
}