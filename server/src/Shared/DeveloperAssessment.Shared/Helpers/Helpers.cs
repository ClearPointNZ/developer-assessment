using System.Text.RegularExpressions;

namespace DeveloperAssessment.Shared.Helpers;

public static class Helpers
{
    /// <summary>
    /// Determines whether a string matches a wildcard pattern.
    /// </summary>
    /// <param name="data">The string to check.</param>
    /// <param name="wildCard">The wildcard pattern to match.</param>
    /// <returns>True if the string matches the wildcard pattern, otherwise false.</returns>
    /// <remarks>Link for more information: https://shassaan.medium.com/implementing-wildcard-string-search-in-linq-c-809af3193ef1</remarks>
    public static bool Matches(this string data, string wildCard)
    {
        var pattern = $"^{wildCard.Replace("*", ".*?")}$";

        return Regex.IsMatch(data, pattern, RegexOptions.IgnoreCase);
    }
}
