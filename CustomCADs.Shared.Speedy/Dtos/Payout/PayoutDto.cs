namespace CustomCADs.Shared.Speedy.Dtos.Payout;

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
