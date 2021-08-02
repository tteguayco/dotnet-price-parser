using System;

namespace DotnetPriceParser
{
    public class Price
    {
        public float? Amount { get; set; }
        public string Currency { get; set; }

        private Price(float? amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public static Price FromString(string rawPrice)
        {
            float? amount = AmountParser.parse(rawPrice);
            string currency = CurrencyParser.parse(rawPrice);

            return new Price(amount, currency);
        }
    }
}
