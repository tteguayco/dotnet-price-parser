using DotnetPriceParser.Helpers;
using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace DotnetPriceParser
{
    internal static class AmountParser
    {
        private static string cleanExtraWhitespaces(string input)
        {
            return Regex.Replace(input, @"\s+", " ").Trim();
        }

        private static string extractAmountText(string input)
        {
            string pattern = @"(?:\.|\,)*(\d+(?:(?:\.|\,)*\d*)*)";

            return RegexHelper.GetFirstMatch(pattern, input);
        }

        private static string joinDetachedDigits(string input)
        {
            return Regex.Replace(input, @"(?<=\d+)\s+(?=\d+)", "").Trim();
        }

        private static string joinDetachedDigitsAndPercentage(string input)
        {
            return Regex.Replace(input, @"(?<=\d+)\s+(?=%)", "").Trim();
        }

        private static string joinDetachedDigitsAndDecimalSeparators(string input)
        {
            return Regex.Replace(input, @"(?<=\d+(?:\.|,))\s+(?=\d+(?:\s|$))", "").Trim();
        }

        private static string replaceNonBreakingSpaces(string input)
        {
            string nonBreakingSpace = Convert.ToChar(65533).ToString();
            return input.Replace(Convert.ToChar(65533).ToString(), " ").Trim();
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

            if (numOfDots == 0 && numOfCommas == 1 && input.IndexOf(',') == input.Length - 4 && input.Length <= 7)
            {
                return EDecimalSeparatorStyle.American;
            }
            else if (numOfCommas == 0 && numOfDots == 1 && input.IndexOf('.') == input.Length - 4 && input.Length <= 7)
            {
                return EDecimalSeparatorStyle.European;
            }

            return EDecimalSeparatorStyle.Unknown;
        }

        private static string preppendLeadingZero(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }
            
            string pattern = @"^(?:\.|,)\d+$";

            if (Regex.IsMatch(input, pattern))
            {
                return "0" + input;
            }

            return input;
        }

        private static string removeRedundantDecimalSeparators(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            input = Regex.Replace(input, @"\.+", ".");
            input = Regex.Replace(input, @"\,+", ",");

            return input.Trim();
        }

        private static string removeWrongDecimalSeparatorsAtBeginning(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            string pattern = @"^(?:\.|\,)\d+(?:\.|\,)\d*$";

            if (Regex.IsMatch(input, pattern))
            {
                return input.Substring(1, input.Length - 1);
            }

            return input;
        }

        private static string removeEuroSymbolAsDecimalSeparator(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            if (!input.Contains("€"))
            {
                return input;
            }

            string decimalSepReplacement = string.Empty;

            if (input.Contains("."))
            {
                decimalSepReplacement = ",";
            }
            else
            {
                decimalSepReplacement = ".";
            }

            string pattern = @"(?<=\d+)€+\s+(?=\d{1,2}(?:\D|$))|(?<=\d+)€+(?=\d+)";

            return Regex.Replace(input, pattern, decimalSepReplacement).Trim();
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

            if (rawPrice.ToLower().Contains("free"))
            {
                return 0;
            }

            rawPrice = cleanExtraWhitespaces(rawPrice);
            rawPrice = replaceNonBreakingSpaces(rawPrice);
            rawPrice = joinDetachedDigits(rawPrice);
            rawPrice = joinDetachedDigitsAndDecimalSeparators(rawPrice);
            rawPrice = discardPercentageAmounts(rawPrice);
            rawPrice = removeEuroSymbolAsDecimalSeparator(rawPrice);

            rawPrice = extractAmountText(rawPrice);

            rawPrice = removeRedundantDecimalSeparators(rawPrice);
            rawPrice = removeWrongDecimalSeparatorsAtBeginning(rawPrice);
            rawPrice = preppendLeadingZero(rawPrice);
            
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
