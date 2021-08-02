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

        public static Price FromString(string rawPrice)
        {
            double? amount = AmountParser.parse(rawPrice);
            string currency = CurrencyParser.parse(rawPrice);

            return new Price(amount, currency);
        }
    }
}
