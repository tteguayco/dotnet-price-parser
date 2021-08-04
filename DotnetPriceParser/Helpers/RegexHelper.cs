using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DotnetPriceParser.Helpers
{
    internal static class RegexHelper
    {
        internal static string GetFirstMatch(string regexPattern, string input)
        {
            Regex regex = new Regex(regexPattern, RegexOptions.None);
            MatchCollection m = regex.Matches(input);

            if (m.Count < 1)
            {
                return null;
            }

            return m[0].Value.Trim();
        }
    }
}
