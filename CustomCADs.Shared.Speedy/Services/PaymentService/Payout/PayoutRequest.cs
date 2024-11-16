namespace CustomCADs.Shared.Speedy.Services.PaymentService.Payout;

public record PayoutRequest(
    string UserName,
    string Password,
    string FromDate,
    string ToDate,
    string? Language,
    long? ClientSystemId,
    bool? IncludeDetails
);
