namespace CustomCADs.Shared.Core.Common.Dtos;

public record MoneyDto(
    decimal Amount,
    string Currency,
    int Precision = 2,
    string? Symbol = default
);
