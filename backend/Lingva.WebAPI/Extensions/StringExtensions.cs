using System;
using System.Text.RegularExpressions;

namespace Lingva.WebAPI.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Gets the name of the variable form pattern ${MyVariable}
        /// </summary>
        /// <returns>The variable name. Example, MyVariable</returns>
        /// <param name="interpolatedVariable">Interpolated variable.</param>
        public static string GetVariableName(this string interpolatedVariable)
        {
            if (string.IsNullOrWhiteSpace(interpolatedVariable))
                throw new ArgumentException(nameof(interpolatedVariable));

            var regexp = new Regex(@"\${(.*)\}");
            Match match = regexp.Match(interpolatedVariable);
            if (!match.Success)
            {
                return interpolatedVariable;
            }

            return match.Groups[1]?.Value?.Trim();
        }
    }
}
