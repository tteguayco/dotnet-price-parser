/*
 * NOTE. This source is based on the _currencies.py file:
 * https://github.com/scrapinghub/price-parser/blob/82c27a8789e627312ed471b519fdd11b7c7cd8c2/price_parser/_currencies.py
 */

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        static CurrencyParser()
        {
            Currencies = JObject.Parse(File.ReadAllText(CurrenciesJsonFilePath));
            CurrenciesReplacedByEuro = JObject.Parse(File.ReadAllText(CurrenciesReplacedByEuroJsonFilePath));

            SetCommonlyUsedUnofficialNames();
            SetUpdates();

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

        public static string parse(string rawPrice)
        {
            return null;
        }
    }
}
