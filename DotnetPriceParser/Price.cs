using System;

namespace DotnetPriceParser
{
    public class Price
    {
        public double? Amount { get; set; }
        public string Currency { get; set; }

        private Price(double? amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public static Price FromString(
            string rawPrice,
            string rawCurrencyHint = null,
            EDecimalSeparatorStyle decSepStyle = EDecimalSeparatorStyle.Unknown)
        {
            double? amount = AmountParser.parse(rawPrice, decSepStyle);
            string currency = CurrencyParser.parse(rawPrice);

            if (string.IsNullOrEmpty(currency) && !string.IsNullOrEmpty(rawCurrencyHint))
            {
                currency = CurrencyParser.parse(rawCurrencyHint);
            }

            return new Price(amount, currency);
        }
    }
}
