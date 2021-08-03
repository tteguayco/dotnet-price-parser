using System.Globalization;
using System.Linq;
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
            string pattern = @"([\d.,]+)";
            Regex regex = new Regex(pattern, RegexOptions.None);
            MatchCollection m = regex.Matches(input);

            if (m.Count < 1)
            {
                return null;
            }

            return m[0].Value;
        }

        private static string joinDetachedDigits(string input)
        {
            return Regex.Replace(input, @"(?<=\d+)\s+(?=\d+)", "").Trim();
        }

        private static string joinDetachedDigitsAndPercentage(string input)
        {
            return Regex.Replace(input, @"(?<=\d+)\s+(?=%)", "").Trim();
        }

        private static string discardPercentageAmounts(string input)
        {
            input = joinDetachedDigitsAndPercentage(input);
            
            return Regex.Replace(input, @"\d+%+", "").Trim();
        }

        private static EDecimalSeparatorStyle GetPredictedDecimalSeparatorStyleByCounting(string input)
        {
            int numOfCommas = input.Count(x => x == ',');
            int numOfDots = input.Count(x => x == '.');

            if (numOfDots == 0 && numOfCommas == 1 && input.IndexOf(',') == input.Length - 4)
            {
                return EDecimalSeparatorStyle.American;
            }
            else if (numOfCommas == 0 && numOfDots == 1 && input.IndexOf('.') == input.Length - 4)
            {
                return EDecimalSeparatorStyle.European;
            }

            return EDecimalSeparatorStyle.Unknown;
        }

        private static EDecimalSeparatorStyle GetPredictedDecimalSeparatorStyle(string input)
        {
            EDecimalSeparatorStyle decSepByCounting = GetPredictedDecimalSeparatorStyleByCounting(input);

            if (decSepByCounting != EDecimalSeparatorStyle.Unknown)
            {
                return decSepByCounting;
            }

            string americanPattern = @"^(?:(?:\d{1,2}\,)?(?:\d{3}\,)*\d{3}(?:\.\d*)?|\d{1,2}\.\d*|\d{1,}\.?\d*)$";
            string europeanPattern = @"^(?:(?:\d{1,2}\.)?(?:\d{3}\.)*\d{3}(?:\,\d*)?|\d{1,2}\,\d*|\d{1,}\,?\d*)$";

            if (Regex.IsMatch(input, americanPattern))
            {
                return EDecimalSeparatorStyle.American;
            }

            if (Regex.IsMatch(input, europeanPattern))
            {
                return EDecimalSeparatorStyle.European;
            }

            return EDecimalSeparatorStyle.Unknown;
        }

        public static double? parse(string rawPrice, EDecimalSeparatorStyle decSepStyle)
        {
            if (string.IsNullOrEmpty(rawPrice))
            {
                return null;
            }

            rawPrice = cleanExtraWhitespaces(rawPrice);
            rawPrice = joinDetachedDigits(rawPrice);
            // TO DO currency symbol as decimal separator; if separator is between two digits
            rawPrice = discardPercentageAmounts(rawPrice);
            rawPrice = extractAmountText(rawPrice);
            
            if (string.IsNullOrEmpty(rawPrice))
            {
                return null;
            }

            if (decSepStyle == EDecimalSeparatorStyle.Unknown)
            {
                decSepStyle = GetPredictedDecimalSeparatorStyle(rawPrice);
            }

            if (decSepStyle == EDecimalSeparatorStyle.Unknown)
            {
                return null;
            }

            CultureInfo cultureInfo = null;

            if (decSepStyle == EDecimalSeparatorStyle.American)
            {
                cultureInfo = new CultureInfo("en-US");
            }
            else if (decSepStyle == EDecimalSeparatorStyle.European)
            {
                cultureInfo = new CultureInfo("de-DE");
            }

            if (cultureInfo == null)
            {
                return null;
            }

            bool conversionSucceeded = double.TryParse(rawPrice,
                NumberStyles.Number, cultureInfo, out double parsedPrice);

            if (!conversionSucceeded)
            {
                return null;
            }

            return parsedPrice;
        }
    }
}
