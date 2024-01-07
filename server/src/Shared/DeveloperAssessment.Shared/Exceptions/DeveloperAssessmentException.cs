namespace DeveloperAssessment.Shared.Exceptions;

/// <summary>
/// Base exception for the DeveloperAssessment application.
/// </summary>
/// <param name="message">The error message</param>
public abstract class DeveloperAssessmentException(string message) : Exception(message);
