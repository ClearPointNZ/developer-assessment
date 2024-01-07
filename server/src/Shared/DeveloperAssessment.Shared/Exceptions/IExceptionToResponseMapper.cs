namespace DeveloperAssessment.Shared.Exceptions;

internal interface IExceptionToResponseMapper
{
    /// <summary>
    /// Maps an exception to a standardised exception response.
    /// </summary>
    /// <param name="exception">The exception to map.</param>
    /// <returns>The mapped exception response.</returns>
    ExceptionResponse Map(Exception exception);
}
