﻿using DotnetPriceParser;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {

        private void assertIsExpectedPrice(string rawInput, string expectedCurrency, double? expectedAmount)
        {
            Price parsedPrice = Price.FromString(rawInput);

            Assert.AreEqual(expectedCurrency, parsedPrice.Currency);
            Assert.AreEqual(expectedAmount, parsedPrice.Amount);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestPriceExtraction()
        {
            assertIsExpectedPrice("67", null, 67);
            assertIsExpectedPrice("naša cena 67,30 €", "€", 67.30);
            assertIsExpectedPrice("EUR29.66 (inc VAT 20% - UK & EEC) EUR24.71 (exc VAT 20% - UK & EEC)", "EUR", 29.66);
            assertIsExpectedPrice("17,00 €", "€", 17);
            assertIsExpectedPrice("0,29 € + iva", "€", 0.29);
            assertIsExpectedPrice("39,00 €", "€", 39);
            assertIsExpectedPrice("24,00 Kč", "Kč", 24);
            assertIsExpectedPrice("běžná cena 7 940 Kč", "Kč", 7940);
            assertIsExpectedPrice("$46.00", "$", 46.00);
            assertIsExpectedPrice("$9.99 & Under", "$", 9.99);
            assertIsExpectedPrice("běžná cena 459,00 Kč", "Kč", 459);
            assertIsExpectedPrice("$60.00", "$", 60);
            assertIsExpectedPrice("0,82 €", "€", 0.82);
            assertIsExpectedPrice("599 Kč", "Kč", 599);
            assertIsExpectedPrice("€10.90", "€", 10.90);
            assertIsExpectedPrice("299,00 EUR", "EUR", 299);
            assertIsExpectedPrice("naša cena 21,95 €", "€", 21.95);
            assertIsExpectedPrice("343,05 €", "€", 343.05);
            assertIsExpectedPrice("1 139,00 €", "€", 1139);
            assertIsExpectedPrice("157,000 تومان", "تومان", 157000);
            assertIsExpectedPrice("35.00", null, 35);
            assertIsExpectedPrice("8.000.000 ₫", "₫", 8000000);
            assertIsExpectedPrice("6790 Dinara", "Dinara", 6790);
            assertIsExpectedPrice("3.99", null, 3.99);
            assertIsExpectedPrice("£ 165.00", "£", 165.00);
            assertIsExpectedPrice("$844,900", "$", 844900);
            assertIsExpectedPrice("140,00 pln", "pln", 140);
            assertIsExpectedPrice("2,99 €", "€", 2.99);
            assertIsExpectedPrice("145,00 lei", "lei", 145);
            assertIsExpectedPrice("3,90 €", "€", 3.90);
            assertIsExpectedPrice("$149.99", "$", 149.99);
            assertIsExpectedPrice("4,30 €", "€", 4.30);
            assertIsExpectedPrice("$36.95", "$", 36.95);
            assertIsExpectedPrice("2 499,00 zł", "zł", 2499);
            assertIsExpectedPrice("800 руб.", "руб.", 800);
            assertIsExpectedPrice("89.00", null, 89);
            assertIsExpectedPrice("3 100р.", "р.", 3100);
            assertIsExpectedPrice("0,85 €", "€", 0.85);
            assertIsExpectedPrice("35,95 €", "€", 35.95);
            assertIsExpectedPrice("$0.42", "$", 0.42);
            assertIsExpectedPrice("80,000 تومان", "تومان", 80000);
            assertIsExpectedPrice("550,00 € *", "€", 550);
            assertIsExpectedPrice("25,00 zł", "zł", 25);
            assertIsExpectedPrice("17,45 EUR", "EUR", 17.45);
            assertIsExpectedPrice("49,00 €", "€", 49);
            assertIsExpectedPrice("169.00", null, 169);
            assertIsExpectedPrice("8,99 €", "€", 8.99);
            assertIsExpectedPrice("1 099 Kč", "Kč", 1099);
            assertIsExpectedPrice("17.99", null, 17.99);
            assertIsExpectedPrice("$274.95", "$", 274.95);
            assertIsExpectedPrice("70,20 €", "€", 70.20);
            assertIsExpectedPrice("289,00 zł", "zł", 289);
            assertIsExpectedPrice("18,00 €", "€", 18);
            assertIsExpectedPrice("12,00 €", "€", 12);
            assertIsExpectedPrice("$19.97", "$", 19.97);
            assertIsExpectedPrice("749,00 euro", "euro", 749);
            assertIsExpectedPrice("$48.25", "$", 48.25);
            assertIsExpectedPrice("5.00", null, 5.00);
            assertIsExpectedPrice("18,00 € *", "€", 18);
            assertIsExpectedPrice("$3.00", "$", 3.00);
            assertIsExpectedPrice("1,85 EUR", "EUR", 1.85);
            assertIsExpectedPrice("4.25", null, 4.25);
            assertIsExpectedPrice("£1.20", "£", 1.20);
            assertIsExpectedPrice("$196.50", "$", 196.50);
            assertIsExpectedPrice("Price: $129.00", "$", 129.00);
            assertIsExpectedPrice("179,00 €", "€", 179.00);
            assertIsExpectedPrice("$80.00", "$", 80.00);
            assertIsExpectedPrice("14.50", null, 14.50);
            assertIsExpectedPrice("From $ 24.95", "$", 24.95);
            assertIsExpectedPrice("$5.11", "$", 5.11);
            assertIsExpectedPrice("EUR 6,99", "EUR", 6.99);
            assertIsExpectedPrice("40% OFF", null, null);
            assertIsExpectedPrice("29.99", null, 29.99);
            assertIsExpectedPrice("14.00€", "€", 14.00);
            assertIsExpectedPrice("22.00", null, 22.00);
            assertIsExpectedPrice("$1000.00", "$", 1000.00);
            assertIsExpectedPrice("$12.95", "$", 12.95);
            assertIsExpectedPrice("běžná cena 987,20 Kč", "Kč", 987.20);
            assertIsExpectedPrice("104,64 zł", "zł", 104.64);
            assertIsExpectedPrice("163,80 €", "€", 163.80);
            assertIsExpectedPrice("$89.00", "$", 89.00);
            assertIsExpectedPrice("1 600 руб.", "руб.", 1600);
            assertIsExpectedPrice("20,95 € *", "€", 20.95);
            assertIsExpectedPrice("9,50 €", "€", 9.50);
            assertIsExpectedPrice("170,00 €", "€", 170);
            assertIsExpectedPrice("170,00€", "€", 170);
            assertIsExpectedPrice("6.00", null, 6.00);
            assertIsExpectedPrice("$24.00", "$", 24.00);
            assertIsExpectedPrice("9.95", null, 9.95);
            assertIsExpectedPrice("34.12 (40.94 inc VAT)", null, 34.12);
            assertIsExpectedPrice("Rp 350.000", "Rp", 350000);
            assertIsExpectedPrice("$55.00", "$", 55.00);
            assertIsExpectedPrice("$595.00", "$", 595.00);
            assertIsExpectedPrice("7,00 €", "€", 7);
            assertIsExpectedPrice("119.95", null, 119.95);
            assertIsExpectedPrice("1.95", null, 1.95);
            assertIsExpectedPrice("390,00 €", "€", 390);
            assertIsExpectedPrice("3.24", null, 3.24);
            assertIsExpectedPrice("12 590 Kč", "Kč", 12590);
            assertIsExpectedPrice("330 Kč", "Kč", 330);
            assertIsExpectedPrice("8 500 руб.", "руб.", 8500);
            assertIsExpectedPrice("589,00 €", "€", 589);
            assertIsExpectedPrice("1,099.99", null, 1099.99);
            assertIsExpectedPrice("14 196 Р", "Р", 14196);
            assertIsExpectedPrice("19.00", null, 19.00);
            assertIsExpectedPrice("870 Kč", "Kč", 870);
            assertIsExpectedPrice("59,00 €", "€", 59);
            assertIsExpectedPrice("Pris från 172 kr", "kr", 172);
            assertIsExpectedPrice("1 573 Kč", "Kč", 1573);
            assertIsExpectedPrice("$2.99", "$", 2.99);
            assertIsExpectedPrice("13,90 €", "€", 13.90);
            assertIsExpectedPrice("29.95", null, 29.95);
            assertIsExpectedPrice("/", null, null);
            assertIsExpectedPrice("16,90 €", "€", 16.90);
            assertIsExpectedPrice("149.95", null, 149.95);
            assertIsExpectedPrice("8.90", null, 8.90);
            assertIsExpectedPrice("419", null, 419);
            assertIsExpectedPrice("$50.00", "$", 50.00);
            assertIsExpectedPrice("3 291,00 €", "€", 3291);
            assertIsExpectedPrice("13,00 €", "€", 13);
            assertIsExpectedPrice("DKK 449,00", "DKK", 449);
            assertIsExpectedPrice("$20.00", "$", 20.00);
            assertIsExpectedPrice("$154", "$", 154);
            assertIsExpectedPrice("22.48", null, 22.48);
            assertIsExpectedPrice("20,00 EUR", "EUR", 20);
            assertIsExpectedPrice("73,460 €", "€", 73460);
            assertIsExpectedPrice("850 руб", "руб", 850);
            assertIsExpectedPrice("$14.99", "$", 14.99);
            assertIsExpectedPrice("$79.95", "$", 79.95);
            assertIsExpectedPrice("40,00 €", "€", 40);
            assertIsExpectedPrice("149,98 €", "€", 149.98);
            assertIsExpectedPrice("1 150 грн.", "грн.", 1150);
            assertIsExpectedPrice("399.00", null, 399.00);
            assertIsExpectedPrice("33,90 €", "€", 33.90);
            assertIsExpectedPrice("79,50 €", "€", 79.50);
            assertIsExpectedPrice("40 130", null, 40130);
            assertIsExpectedPrice("$69.99", "$", 69.99);
            assertIsExpectedPrice("1 090 Kč", "Kč", 1090);
            assertIsExpectedPrice("395 Kč", "Kč", 395);
            assertIsExpectedPrice("53,95 €", "€", 53.95);
            assertIsExpectedPrice("£0.99", "£", 0.99);
            assertIsExpectedPrice("5,60 € *", "€", 5.60);
            assertIsExpectedPrice("29,50 zł", "zł", 29.50);
            assertIsExpectedPrice("2 990", null, 2990);
            assertIsExpectedPrice("0,00", null, 0);
            assertIsExpectedPrice("$24.99 with card", "$", 24.99);
            assertIsExpectedPrice("18,00€", "€", 18);
            assertIsExpectedPrice("€600,00", "€", 600);
            assertIsExpectedPrice("£25.00 (tax incl.)", "£", 25);
            assertIsExpectedPrice("8,55 €", "€", 8.55);
            assertIsExpectedPrice("1,422.50", null, 1422.50);
            assertIsExpectedPrice("244,00 €", "€", 244.00);
            assertIsExpectedPrice("12,90 €", "€", 12.90);
            assertIsExpectedPrice("12 900,00 руб", "руб", 12900);
            assertIsExpectedPrice("1.727 Ft", "Ft", 1727);
            assertIsExpectedPrice("79,00 €", "€", 79);
            assertIsExpectedPrice("NZD $100.70", "NZD", 100.70);
            assertIsExpectedPrice("479.00", null, 479.00);
            assertIsExpectedPrice("$ 69.00", "$", 69.00);
            assertIsExpectedPrice("135,00 €", "€", 135.00);
            assertIsExpectedPrice("25.00", null, 25.0);
            assertIsExpectedPrice("94,90 €", "€", 94.90);
            assertIsExpectedPrice("149.99", null, 149.99);
            assertIsExpectedPrice("44,00 €", "€", 44.00);
            assertIsExpectedPrice("$24.99", "$", 24.99);
            assertIsExpectedPrice("22,00 EUR", "EUR", 22.00);
            assertIsExpectedPrice("89,90 €", "€", 89.90);
            assertIsExpectedPrice("$24.95", "$", 24.95);
            assertIsExpectedPrice("£ 1.99", "£", 1.99);
            assertIsExpectedPrice("1 099,00 zł", "zł", 1099);
            assertIsExpectedPrice("běžná cena 28 270,00 Kč", "Kč", 28270);
            assertIsExpectedPrice("da € 72.00", "€", 72.00);
            assertIsExpectedPrice("$15.95", "$", 15.95);
            assertIsExpectedPrice("تومان56,000", "تومان", 56000);
            assertIsExpectedPrice("$1,695.00", "$", 1695.00);
            assertIsExpectedPrice("£595.00", "£", 595.00);
            assertIsExpectedPrice("$11.95", "$", 11.95);
            assertIsExpectedPrice("290,00 Kč", "Kč", 290);
            assertIsExpectedPrice("199.90 Fr.", "Fr.", 199.90);
            assertIsExpectedPrice("197 PLN", "PLN", 197);
            assertIsExpectedPrice("9.99", null, 9.99);
            assertIsExpectedPrice("$56.00", "$", 56.00);
            assertIsExpectedPrice("4 980 Kč", "Kč", 4980);
            assertIsExpectedPrice("124,00 €", "€", 124);
            assertIsExpectedPrice("$104.99", "$", 104.99);
            assertIsExpectedPrice("39,00 €", "€", 39);
            assertIsExpectedPrice("1 029,00 €", "€", 1029);
            assertIsExpectedPrice("Běžná cena 299,00 Kč", "Kč", 299);
            assertIsExpectedPrice("745,00 €", "€", 745);
            assertIsExpectedPrice("$89.00", "$", 89);
            assertIsExpectedPrice("$29.95", "$", 29.95);
            assertIsExpectedPrice("2.00", null, 2.00);
            assertIsExpectedPrice("249.99", null, 249.99);
            assertIsExpectedPrice("24.99", null, 24.99);
            assertIsExpectedPrice("1 499 Kč", "Kč", 1499);
            assertIsExpectedPrice("199,95 €", "€", 199.95);
            assertIsExpectedPrice("6,00 €", "€", 6);
            assertIsExpectedPrice("$28.49", "$", 28.49);
            assertIsExpectedPrice("200.000 đ", "đ", 200000);
            assertIsExpectedPrice("9,24 €", "€", 9.24);
            assertIsExpectedPrice("48,00 €", "€", 48.00);
            assertIsExpectedPrice("Cena : 890 Kč", "Kč", 890);
            assertIsExpectedPrice("790.00", null, 790.00);
            assertIsExpectedPrice("17 260 руб.", "руб.", 17260);
            assertIsExpectedPrice("227,000 تومان", "تومان", 227000);
            assertIsExpectedPrice("295,88 €", "€", 295.88);
            assertIsExpectedPrice("£1399", "£", 1399);
            assertIsExpectedPrice("11,33 Br", "Br", 11.33);
            assertIsExpectedPrice("325.95", null, 325.95);
            assertIsExpectedPrice("$19.50", "$", 19.50);
            assertIsExpectedPrice("19,00 €", "€", 19);
            assertIsExpectedPrice("2 999,00 €", "€", 2999);
            assertIsExpectedPrice("49.95", null, 49.95);
            assertIsExpectedPrice("99 LEI", "LEI", 99);
            assertIsExpectedPrice("249 Kč", "Kč", 249);
            assertIsExpectedPrice("3.79", null, 3.79);
            assertIsExpectedPrice("běžná cena 890 Kč", "Kč", 890);
            assertIsExpectedPrice("$809,000", "$", 809000);
            assertIsExpectedPrice("450 000 ₫", "₫", 450000);
            assertIsExpectedPrice("30,00 €", "€", 30.00);
            assertIsExpectedPrice("14.95", null, 14.95);
            assertIsExpectedPrice("12.50", null, 12.50);
            assertIsExpectedPrice("129,00 € (-15%)", "€", 129.00);
            assertIsExpectedPrice("12,90 €", "€", 12.90);
            assertIsExpectedPrice("A partir de 11,70 €", "€", 11.70);
            assertIsExpectedPrice("15.49", null, 15.49);
            assertIsExpectedPrice("12.34 €", "€", 12.34);
            assertIsExpectedPrice("€799,00", "€", 799);
            assertIsExpectedPrice("230 лв.", "лв.", 230);
            assertIsExpectedPrice("14.55 €", "€", 14.55);
            assertIsExpectedPrice("133,86 LEI", "LEI", 133.86);
            assertIsExpectedPrice("7 990,00 Kč", "Kč", 7990);
            assertIsExpectedPrice("350.00", null, 350.00);
            assertIsExpectedPrice("Cena: 55,72 zł brutto", "zł", 55.72);
            assertIsExpectedPrice("O blenderach Omniblend", null, null);
            assertIsExpectedPrice("3,822.00", null, 3822);
            assertIsExpectedPrice("0,15 €", "€", 0.15);
            assertIsExpectedPrice("430,00 €", "€", 430);
            assertIsExpectedPrice("$29.00", "$", 29.00);
            assertIsExpectedPrice("39.99", null, 39.99);
            assertIsExpectedPrice("$15.00", "$", 15.00);
            assertIsExpectedPrice("21,00 Lei", "Lei", 21.00);
            assertIsExpectedPrice("naše cena 250,00 Kč", "Kč", 250.00);
            assertIsExpectedPrice("$24.95", "$", 24.95);
            assertIsExpectedPrice("162.18", null, 162.18);
            assertIsExpectedPrice("39,60 EUR", "EUR", 39.60);
            assertIsExpectedPrice("10,75 €", "€", 10.75);
            assertIsExpectedPrice("219 руб.", "руб.", 219);
            assertIsExpectedPrice("89,00 € *", "€", 89.00);
            assertIsExpectedPrice("151,200 تومان", "تومان", 151200);
            assertIsExpectedPrice("$159.99", "$", 159.99);
            assertIsExpectedPrice("2.49", null, 2.49);
            assertIsExpectedPrice("7.38", null, 7.38);
            assertIsExpectedPrice("62,00 zł", "zł", 62.00);
            assertIsExpectedPrice("$20.00", "$", 20);
            assertIsExpectedPrice("$ 50.00", "$", 50);
            assertIsExpectedPrice("34.99", null, 34.99);
            assertIsExpectedPrice("318,00 €", "€", 318);
            assertIsExpectedPrice("11.499,00 EUR", "EUR", 11499);
            assertIsExpectedPrice("€ 75.00", "€", 75.00);
            assertIsExpectedPrice("11,90 € *", "€", 11.90);
            assertIsExpectedPrice("€0.51", "€", 0.51);
            assertIsExpectedPrice("6,50 €", "€", 6.50);
            assertIsExpectedPrice("790 Kč", "Kč", 790);
            assertIsExpectedPrice("ab 2.99 €", "€", 2.99);
            assertIsExpectedPrice("369", null, 369);
            assertIsExpectedPrice("134.96", null, 134.96);
            assertIsExpectedPrice("135 lei", "lei", 135);
            assertIsExpectedPrice("2,99 € *", "€", 2.99);
            assertIsExpectedPrice("$9.99", "$", 9.99);
            assertIsExpectedPrice("2.950,00 €", "€", 2950);
            assertIsExpectedPrice("19.99", null, 19.99);
            assertIsExpectedPrice("49 lei", "lei", 49);
            assertIsExpectedPrice("31,07 € (bez DPH)", "€", 31.07);
            assertIsExpectedPrice("56.00", null, 56.00);
            assertIsExpectedPrice("54.95", null, 54.95);
            assertIsExpectedPrice("$ 80.00", "$", 80.00);
            assertIsExpectedPrice("$39.00", "$", 39.00);
            assertIsExpectedPrice("Rp 221.000", "Rp", 221000);
            assertIsExpectedPrice("35,90 EUR", "EUR", 35.90);
            assertIsExpectedPrice("4 835,50 €", "€", 4835.50);
            assertIsExpectedPrice("75,00€", "€", 75);
            assertIsExpectedPrice("$21.95", "$", 21.95);
            assertIsExpectedPrice("737,00", null, 737);
            assertIsExpectedPrice("129,00 € **", "€", 129);
            assertIsExpectedPrice("2 399 Kč", "Kč", 2399);
            assertIsExpectedPrice("430 руб", "руб", 430);
            assertIsExpectedPrice("69.95", null, 69.95);
            assertIsExpectedPrice("$0.00", "$", 0);
            assertIsExpectedPrice("49.56", null, 49.56);
            assertIsExpectedPrice("0,00 EUR", "EUR", 0);
            assertIsExpectedPrice("145,00 Kč", "Kč", 145);
            assertIsExpectedPrice("99,00 lei", "lei", 99);
            assertIsExpectedPrice("$750,000", "$", 750000);
            assertIsExpectedPrice("$49.99", "$", 49.99);
            assertIsExpectedPrice("29.00", null, 29.00);
            assertIsExpectedPrice("$7.20", "$", 7.20);
            assertIsExpectedPrice("69.00", null, 69.00);
            assertIsExpectedPrice("4.47", null, 4.47);
            assertIsExpectedPrice("39,90 € *", "€", 39.90);
            assertIsExpectedPrice("469,00 €", "€", 469);
            assertIsExpectedPrice("24.38", null, 24.38);
            assertIsExpectedPrice("6,24", null, 6.24);
            assertIsExpectedPrice("$89.00", "$", 89.00);
            assertIsExpectedPrice("24,35 €", "€", 24.35);
            assertIsExpectedPrice("Pris från 805 kr", "kr", 805);
            assertIsExpectedPrice("295 Kč", "Kč", 295);
            assertIsExpectedPrice("175.00", null, 175.00);
            assertIsExpectedPrice("7 990 kr", "kr", 7990);
            assertIsExpectedPrice("14,00 €", "€", 14);
            assertIsExpectedPrice("249 Kč", "Kč", 249);
            assertIsExpectedPrice("£39.95", "£", 39.95);
            assertIsExpectedPrice("10,75 TL", "TL", 10.75);
            assertIsExpectedPrice("$25.00", "$", 25.00);
            assertIsExpectedPrice("1 720,00 zł", "zł", 1720);
            assertIsExpectedPrice("běžná cena 749 Kč", "Kč", 749);
            assertIsExpectedPrice("425,00 €", "€", 425);
            assertIsExpectedPrice("59.00", null, 59.00);
            assertIsExpectedPrice("1,120.00", null, 1120);
            assertIsExpectedPrice("a partire da 7,32 € *", "€", 7.32);
            assertIsExpectedPrice("148.50 Inc GST", null, 148.50);
            assertIsExpectedPrice("80.00", null, 80.00);
            assertIsExpectedPrice("93 499 Kč", "Kč", 93499);
            assertIsExpectedPrice("1.599,00 € *", "€", 1599);
            assertIsExpectedPrice("ab 3,63 EUR", "EUR", 3.63);
            assertIsExpectedPrice("29,90 EUR", "EUR", 29.90);
            assertIsExpectedPrice("$3.95", "$", 3.95);
            assertIsExpectedPrice("3430 лв.", "лв.", 3430);
            assertIsExpectedPrice("724,00 €", "€", 724);
            assertIsExpectedPrice("18,00 €", "€", 18);
            assertIsExpectedPrice("6,75 €", "€", 6.75);
            assertIsExpectedPrice("29,90 € *", "€", 29.90);
            assertIsExpectedPrice("135.99", null, 135.99);
            assertIsExpectedPrice("30,000 تومان", "تومان", 30000);
            assertIsExpectedPrice("1 500 Kč", "Kč", 1500);
            assertIsExpectedPrice("349,00 €", "€", 349);
            assertIsExpectedPrice("$250.00", "$", 250.00);
            assertIsExpectedPrice("44.95", null, 44.95);
            assertIsExpectedPrice("$22.75", "$", 22.75);
            assertIsExpectedPrice("250,00 €", "€", 250);
            assertIsExpectedPrice("14.96 €", "€", 14.96);
            assertIsExpectedPrice("$4,350.00", "$", 4350);
            assertIsExpectedPrice("379 Kč", "Kč", 379);
            assertIsExpectedPrice("19,50 EUR", "EUR", 19.5);
            assertIsExpectedPrice("33,68 zł", "zł", 33.68);
            assertIsExpectedPrice("6.70€", "€", 6.70);
            assertIsExpectedPrice("$29.99", "$", 29.99);
            assertIsExpectedPrice("6.50", null, 6.50);
        }
    }
}