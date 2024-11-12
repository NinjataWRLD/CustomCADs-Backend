namespace CustomCADs.Shared.Core.Domain.ValueObjects;

using static Constants.Money;

public record Money
{
    public Money() : this(0m, string.Empty) { }
    public Money(decimal amount, string currency, int precision = 2, string? symbol = default)
    {
        Precision = ValidatePreision(precision);
        Amount = Math.Round(amount, precision);
        Currency = ValidateCurrency(currency.ToUpperInvariant());
        Symbol = symbol ?? GetCurrencySymbol(currency);
    }

    public decimal Amount { get; }
    public string Currency { get; }
    public int Precision { get; }
    public string Symbol { get; }

    public Money Multiply(int factor)
        => new(Amount * factor, Currency, Precision, Symbol);

    private static int ValidatePreision(in int precision)
    {
        if (precision > PrecisionMax || precision < PrecisionMin)
        {
            string message = $"Money's Precision must be between {PrecisionMin} and {PrecisionMax}";
            throw new ArgumentException(message, nameof(precision));
        }

        return precision;
    }

    private static string ValidateCurrency(in string currency)
        => currency ?? throw new ArgumentNullException(nameof(currency));

    private static string GetCurrencySymbol(string currency)
        => currency.ToUpperInvariant() switch
        {
            "BGN" => "лв",
            "USD" => "$",
            "EUR" => "€",
            "GBP" => "£",
            "CHF" => "Fr",
            "JPY" or "CNY" => "¥",
            _ => currency,
        };

}
