using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BackgroundRedditConsumer.Infrastructure;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        ProblemDetails problemDetails;

        _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

        if (exception is ValidationException fluentException)
        {
            problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "Validation error",
                Detail = "One or more validation errors has occurred"
            };

            if (fluentException.Errors is not null)
            {
                problemDetails.Extensions["errors"] = fluentException.Errors;
            }

            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
        else
        {
            problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Server Error"
            };

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
