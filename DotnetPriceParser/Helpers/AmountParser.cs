using System.Globalization;
using System.Text.RegularExpressions;

namespace DotnetPriceParser
{
    public static class AmountParser
    {
        private static string cleanExtraWhitespaces(string input)
        {
            return Regex.Replace(input, @"\s+", " ").Trim();
        }

        private static string extractAmountText(string input)
        {
            string pattern = @"([\d\s.,]+)";
            Regex regex = new Regex(pattern, RegexOptions.None);
            Match m = regex.Match(input);

            if (m == null || m.Groups.Count <= 1)
            {
                return null;
            }

            return m.Groups[1].Value;
        }

        private static string joinDetachedDigits(string input)
        {
            return Regex.Replace(input, @"(?<=\d+)\s+(?=\d+)", "").Trim();
        }

        private static EDecimalSeparatorStyle GetDecimalSeparator(string input)
        {
            string europeanPattern = @"^(((?:\d{1,2}\.)?(?:\d{3}\.)*\d{1,3})|(\d{1,}))(?:,\d*)?$";
            string americanPattern = @"^(((?:\d{1,2}\,)?(?:\d{3}\,)*\d{1,3})|(\d{1,}))(?:.\d*)?$";

            if (Regex.IsMatch(input, europeanPattern))
            {
                return EDecimalSeparatorStyle.European;
            }
            
            if (Regex.IsMatch(input, americanPattern))
            {
                return EDecimalSeparatorStyle.American;
            }

            return EDecimalSeparatorStyle.Unknown;
        }

        public static double? parse(string rawPrice)
        {
            if (string.IsNullOrEmpty(rawPrice))
            {
                return null;
            }

            rawPrice = cleanExtraWhitespaces(rawPrice);
            rawPrice = extractAmountText(rawPrice);
            rawPrice = joinDetachedDigits(rawPrice);

            if (string.IsNullOrEmpty(rawPrice))
            {
                return null;
            }

            EDecimalSeparatorStyle decSep = GetDecimalSeparator(rawPrice);

            if (decSep == EDecimalSeparatorStyle.Unknown)
            {
                return null;
            }

            CultureInfo cultureInfo = null;

            if (decSep == EDecimalSeparatorStyle.European)
            {
                cultureInfo = new CultureInfo("de-DE");
            }
            else if (decSep == EDecimalSeparatorStyle.American)
            {
                cultureInfo = new CultureInfo("en-US");
            }

            if (cultureInfo == null)
            {
                return null;
            }

            double parsedPrice;
            bool conversionSucceeded = double.TryParse(rawPrice, 
                NumberStyles.AllowCurrencySymbol, cultureInfo, out parsedPrice);

            if (conversionSucceeded)
            {
                return parsedPrice;
            }

            return null;
        }
    }
}
