using System.Net;

namespace DeveloperAssessment.Shared.Exceptions;

/// <summary>
/// Represents a response for an exception.
/// </summary>
/// <param name="Response">The error response object.</param>
/// <param name="StatusCode">The status code.</param> 
internal sealed record ExceptionResponse(object Response, HttpStatusCode StatusCode);
