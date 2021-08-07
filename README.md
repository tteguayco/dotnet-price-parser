# dotnet-price-parser

The latest release can be downloaded via **[NuGet]()**.

![Open Source Love](https://badges.frapsoft.com/os/v1/open-source.svg?v=103) [![Awesome](https://cdn.rawgit.com/sindresorhus/awesome/d7305f38d29fed78fa85652e3a63e154dd8e8829/media/badge.svg)](https://github.com/sindresorhus/awesome)

```dotnet-price-parser``` is an open source .NET library for parsing scraped prices. Save time tuning your XPath and CSS selectors to get amount and currency accuratedly parsed out of raw string texts.

This project is based on the open source Python library [```price-parser```](https://github.com/scrapinghub/price-parser), by ScrapingHub.

## Features

* Prediction of thousands and decimal separator, if not specified.
* Support for +150 currencies.
* Currency hint specification as an alternate source for currency lookup.

## Usage

![C#](https://img.shields.io/badge/C%23-8.0-yellowgreen)

### Examples
```cs
Price parsedPrice = Price.FromString("Price: $119.00");

double? amount = parsedPrice.Amount        // 119.00
string currency = parsedPrice.Currency     // "$"
```

```cs
Price price = Price.FromString("Price: $119.00")
// price.Amount = 119.00; price.Currency = "$"
```

```cs
Price price = Price.FromString("15 130 Р")
// price.Amount = 15130; price.Currency = "Р"
```

```cs
Price price = Price.FromString("151,200 تومان")
// price.Amount = 151200; price.Currency = "تومان"
```

```cs
Price price = Price.FromString("Rp 1.550.000")
// price.Amount = 15500000; price.Currency = "Rp"
```

```cs
Price price = Price.FromString("Běžná cena 75 990,00 Kč")
// price.Amount = 75990; price.Currency = "Kč"
```

### Amount or currency not found

If either the amount or the currency are not found, the corresponding property for that price is set to ```null```.

```cs
Price price = Price.FromString("Foo")
// price.Amount = null; price.Currency = null
```

### Special cases

```cs
Price price = Price.FromString("Free")
// price.Amount = 0.0; price.Currency = null
```

```cs
Price price = Price.FromString("40% OFF")
// price.Amount = null; price.Currency = null
```

### Currency hint

An alternate source string can be specified to try to extract the currency, if the latter could not be found in the provided raw price:

```cs
Price price = Price.FromString("8.29", currencyHint: "£ 8.29");
// price.Amount = 8.29; price.Currency = "£"
```

```cs
Price price = Price.FromString("8.29 EUR", currencyHint: "£ 8.29");
// price.Amount = 8.29; price.Currency = "EUR"
```

### Decimal separator style

The library tries to predict both decimal and thousands separators but if this information is known beforehand, you can specify the style that is used:

```cs
Price price = Price.FromString("Price: $140.600", EDecimalSeparatorStyle.American)
// price.Amount = 140.6; price.Currency = "USD"
```

```cs
Price price = Price.FromString("Price: $140.600", EDecimalSeparatorStyle.European)
// price.Amount = 140600; price.Currency = "USD"
```

```cs
Price price = Price.FromString("Price: $140.600")
// price.Amount = 140600; price.Currency = "USD"
```

## Running the tests

The library is tested with +900 [NUnit](https://nunit.org/) tests, which can be easily run from Visual Studio.

## Donation
If this project helps you reduce developing time, you may consider giving me a cup of coffee :-)

[![Donate](https://www.oldpathschapel.com/wp-content/uploads/2020/03/paypal-donate-button-300x116.png)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=5ENPSGHWL3AQ8&source=url)
