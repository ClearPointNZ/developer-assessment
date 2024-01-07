using Humanizer;
using System.Collections.Concurrent;
using System.Net;

namespace DeveloperAssessment.Shared.Exceptions;

internal sealed class ExceptionToResponseMapper : IExceptionToResponseMapper
{
    // https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentdictionary-2.getoradd?view=net-8.0 -> Remarks
    // This explains why we use Lazy<string> instead of string
    private static readonly ConcurrentDictionary<Type, Lazy<string>> Codes = new();

    /// <inheritdoc />
    public ExceptionResponse Map(Exception exception) => exception switch
    {
        DeveloperAssessmentException ex => new ExceptionResponse(new ErrorsResponse(new Error(GetErrorCode(ex), ex.Message)), HttpStatusCode.BadRequest),
        _ => new ExceptionResponse(new ErrorsResponse(new Error("error", "There was an error")), HttpStatusCode.InternalServerError)
    };

    private sealed record Error(string Code, string Message);

    private sealed record ErrorsResponse(params Error[] Errors);

    private static string GetErrorCode(object exception)
    {
        var exceptionType = exception.GetType();
        var exceptionName = exceptionType.Name.Underscore().Replace("_exception", string.Empty);

        return Codes.GetOrAdd(exceptionType, new Lazy<string>(() => exceptionName)).Value;
    }
}
