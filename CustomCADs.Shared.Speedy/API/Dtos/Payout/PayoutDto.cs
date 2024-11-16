namespace CustomCADs.Shared.Speedy.API.Dtos.Payout;

using Enums;

public record PayoutDto(
    string Date,
    long DocId,
    ProcessingType DocType,
    PaymentType PaymentType,
    string Payee,
    string Currency,
    double Amount,
    PayoutDetailsDto[] Details
);
