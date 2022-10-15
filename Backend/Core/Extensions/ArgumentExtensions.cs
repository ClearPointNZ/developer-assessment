﻿using System.Runtime.CompilerServices;

namespace TodoApp.Extensions
{
    public static class ArgumentExtensions
    {
        public static string EnsureNotNullOrWhiteSpace(this string argument, [CallerArgumentExpression("parameter")] string? argumentName = null)
        {
            if(string.IsNullOrWhiteSpace(argument))
            {
                throw new ArgumentNullException(nameof(argumentName));
            }
            return argument;
        }
    }
}
