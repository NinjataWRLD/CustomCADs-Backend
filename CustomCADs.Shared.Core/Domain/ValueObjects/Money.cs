using System.ComponentModel.DataAnnotations;

namespace CustomCADs.Shared.Core.Domain.ValueObjects;

public record Money
{
    public Money() : this(0m, string.Empty) { }
    public Money(decimal amount, string currency, int precision = 2, string? symbol = default)
    {
        Amount = Math.Round(amount, precision);
        Currency = currency.ToUpperInvariant();
        Precision = precision;
        Symbol = symbol ?? GetCurrencySymbol(currency);
    }

    public decimal Amount { get; }
    public string Currency { get; }
    public int Precision { get; }
    public string Symbol { get; }

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
    
    public Money Multiply(int factor)
        => new(Amount * factor, Currency, Precision, Symbol);

}
