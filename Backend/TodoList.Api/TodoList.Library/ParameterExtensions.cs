using System.Runtime.CompilerServices;

namespace TodoList.Library;

public static class ParameterExtensions
{
    public static T EnsureNotNull<T>(
        this T parameter
        , [CallerArgumentExpression("parameter")] string? parameterName = null)
    {
        if (parameter is null)
            throw new ArgumentNullException(parameterName);

        return parameter;
    }

    public static T EnsureNotNullOrDefault<T>(
        this T parameter
        , [CallerArgumentExpression("parameter")] string? parameterName = null)
    {
        EnsureNotNull(parameter, parameterName);

        if (EqualityComparer<T>.Default.Equals(parameter, default))
            throw new ArgumentNullException(parameterName);

        return parameter!;
    }
}