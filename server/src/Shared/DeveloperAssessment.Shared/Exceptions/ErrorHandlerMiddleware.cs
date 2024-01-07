using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace DeveloperAssessment.Shared.Exceptions;

/// <summary>
/// Middleware to handle global exceptions and return a structured response
/// </summary>
/// <param name="exceptionToResponseMapper">An exception mapper</param>
/// <param name="logger">The logger</param>
internal sealed class ErrorHandlerMiddleware(IExceptionToResponseMapper exceptionToResponseMapper, ILogger<ErrorHandlerMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "{Message}", exception.Message);

            await HandleErrorAsync(context, exception);
        }
    }

    private async Task HandleErrorAsync(HttpContext context, Exception exception)
    {
        var errorResponse = exceptionToResponseMapper.Map(exception);

        context.Response.StatusCode = (int)(errorResponse?.StatusCode ?? HttpStatusCode.InternalServerError);

        var response = errorResponse?.Response;

        if (response is null)
        {
            return;
        }

        await context.Response.WriteAsJsonAsync(response);
    }
}
