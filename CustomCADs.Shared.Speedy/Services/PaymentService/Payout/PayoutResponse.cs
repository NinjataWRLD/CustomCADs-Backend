namespace CustomCADs.Shared.Speedy.Services.PaymentService.Payout;

using Dtos.Errors;
using Dtos.Payout;

public record PayoutResponse(
    PayoutDto[] Payouts,
    ErrorDto? Error
);
