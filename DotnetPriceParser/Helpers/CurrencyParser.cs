/*
 * NOTE. This source is based on the _currencies.py file:
 * https://github.com/scrapinghub/price-parser/blob/82c27a8789e627312ed471b519fdd11b7c7cd8c2/price_parser/_currencies.py
 */

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace DotnetPriceParser
{
    public static class CurrencyParser
    {
        private static readonly string CurrenciesJsonFilePath = @"Resources/currencies.json";
        private static readonly string CurrenciesReplacedByEuroJsonFilePath = @"Resources/currencies_replaced_by_euro.json";

        private static JObject Currencies;
        private static JObject CurrenciesReplacedByEuro;

        private static List<string> CurrencyCodes;
        private static List<string> CurrencySymbols;
        private static List<string> CurrencyNationalSymbols;

        private static List<string> UnsafeCurrencySymbols = new List<string>();
        private static readonly List<string> SafeCurrencySymbols = new List<string>()
        {
            // Variants of $, etc. They need to be before $.
            "Bds$", "CUC$", "MOP$",
            "AR$", "AU$", "BN$", "BZ$", "CA$", "CL$", "CO$", "CV$", "HK$", "MX$",
            "NT$", "NZ$", "TT$", "RD$", "WS$", "US$",
            "$U", "C$", "J$", "N$", "R$", "S$", "T$", "Z$", "A$",
            "SY£", "LB£", "CN¥", "GH₵",

            // Unique currency symbols
            "$", "€", "£", "zł", "Zł", "Kč", "₽", "¥", "￥",
            "฿", "դր.", "դր", "₦", "₴", "₱", "৳", "₭", "₪",  "﷼", "៛", "₩", "₫", "₡",
            "টকা", "ƒ", "₲", "؋", "₮", "नेरू", "₨",
            "₶", "₾", "֏", "ރ", "৲", "૱", "௹", "₠", "₢", "₣", "₤", "₧", "₯",
            "₰", "₳", "₷", "₸", "₹", "₺", "₼", "₾", "₿", "ℳ",
            "ر.ق.\u200f", "د.ك.\u200f", "د.ع.\u200f", "ر.ع.\u200f", "ر.ي.\u200f",
            "ر.س.\u200f", "د.ج.\u200f", "د.م.\u200f", "د.إ.\u200f", "د.ت.\u200f",
            "د.ل.\u200f", "ل.س.\u200f", "د.ب.\u200f", "د.أ.\u200f", "ج.م.\u200f",
            "ل.ل.\u200f",

            " تومان", "تومان",

            // Other common symbols, which we consider unambiguous
            "EUR", "euro", "eur", "CHF", "DKK", "Rp", "lei",
            "руб.", "руб",  "грн.", "грн", "дин.", "Dinara", "динар", "лв.", "лв",
            "р.", "тңг", "тңг.", "ман."
        };

        static CurrencyParser()
        {
            Currencies = JObject.Parse(File.ReadAllText(CurrenciesJsonFilePath));
            CurrenciesReplacedByEuro = JObject.Parse(File.ReadAllText(CurrenciesReplacedByEuroJsonFilePath));

            SetCommonlyUsedUnofficialNames();
            SetUpdates();
            InitCurrencyData();
            InitUnsafeCurrencySymbols();
        }

        private static void SetCommonlyUsedUnofficialNames()
        {
            // See also: https://en.wikipedia.org/wiki/ISO_4217#Unofficial_currency_codes
            Currencies.Add("NTD", Currencies.Value<JObject>("TWD"));
            Currencies.Add("RMB", Currencies.Value<JObject>("CNY"));
        }

        private static void SetUpdates()
        {
            foreach (var curr in CurrenciesReplacedByEuro)
            {
                if (Currencies.ContainsKey(curr.Key))
                {
                    Currencies[curr.Key] = curr.Value;
                }
                else
                {
                    Currencies.Add(curr.Key, curr.Value);
                }
            }

            Currencies["VND"]["sn2"] = new JArray() { "đ" };
            Currencies["RON"]["sn2"] = new JArray() { "lei", "leu", "Lei", "LEI" };
            Currencies["CHF"]["sn2"] = new JArray() { "Fr." };
            Currencies["PLN"]["sn2"] = new JArray() { "pln" };
            Currencies["INR"]["sn2"] = new JArray() { "₹", "र" };
            Currencies["IRR"]["sn2"] = new JArray() { "ریال" };
        }

        private static void InitCurrencyData()
        {
            CurrencyCodes = Currencies.Properties().Select(p => p.Name).Distinct().ToList();
            CurrencySymbols = Currencies.Properties().Select(p => (string)p.Value["s"]).Distinct().ToList();
            CurrencyNationalSymbols = Currencies.Properties().Select(p => (string)p.Value["sn"]).Distinct().ToList();

            var otherCurrencySymbols = Currencies.Properties()
                .Where(p => p.Value["sn2"] != null).Select(p => p.Value["sn2"]).Distinct().ToList();

            foreach (var symbolList in otherCurrencySymbols)
            {
                foreach (var symbol in symbolList)
                {
                    CurrencyNationalSymbols.Add((string)symbol);
                }
            }
        }

        private static void InitUnsafeCurrencySymbols()
        {
            HashSet<string> otherCurrencySymbolsSet = new HashSet<string>();

            otherCurrencySymbolsSet.UnionWith(CurrencyCodes);
            otherCurrencySymbolsSet.UnionWith(CurrencySymbols);
            otherCurrencySymbolsSet.UnionWith(CurrencyNationalSymbols);
            otherCurrencySymbolsSet.UnionWith(new List<string>() { "р", "Р" });
            otherCurrencySymbolsSet.ExceptWith(SafeCurrencySymbols);
            otherCurrencySymbolsSet.ExceptWith(new List<string>() { "-", "XXX" });
            otherCurrencySymbolsSet.ExceptWith("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray().Select(s => s.ToString()));

            UnsafeCurrencySymbols = otherCurrencySymbolsSet.OrderByDescending(s => s.Length).ToList<string>();
        }

        public static string parse(string rawPrice)
        {
            string safeCurrencySearchPattern = string.Join("|", SafeCurrencySymbols.Select(s => Regex.Escape(s)));
            string unSafeCurrencySearchPattern = string.Join("|", UnsafeCurrencySymbols.Select(s => Regex.Escape(s)));

            Regex regex = new Regex(safeCurrencySearchPattern, RegexOptions.None);
            MatchCollection m = regex.Matches(rawPrice);
            string result = null;

            if (m.Count < 1)
            {
                regex = new Regex(unSafeCurrencySearchPattern, RegexOptions.None);
                m = regex.Matches(rawPrice);

                if (m.Count < 1)
                {
                    return null;
                }

                result = m[0].Value;
            }

            result = m[0].Value;

            return result.Trim();
        }
    }
}
