using CustomCADs.Shared.Core.Domain.ValueObjects;

namespace CustomCADs.Shared.Core.Dtos;

public record MoneyDto(
    decimal Amount,
    string Currency,
    int Precision = 2,
    string? Symbol = default
)
{
    public MoneyDto() : this(
        Amount: 0,
        Currency: string.Empty,
        Precision: 0,
        Symbol: null
    )
    { }

    public MoneyDto(Money money) : this(
        Amount: money.Amount,
        Currency: money.Currency,
        Precision: money.Precision,
        Symbol: money.Symbol
    )
    { }
};
