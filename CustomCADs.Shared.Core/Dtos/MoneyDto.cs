namespace CustomCADs.Shared.Core.Dtos;

public record MoneyDto(
    decimal Amount,
    string Currency,
    int Precision = 2,
    string? Symbol = default
);
