namespace CustomCADs.Shared.Speedy.Services.Payment.Models;

public record PayoutModel(
    DateOnly Date,
    long DocId,
    ProcessingType DocType,
    PaymentType PaymentType,
    string Payee,
    string Currency,
    double Amount,
    PayoutDetailsModel[] Details
);