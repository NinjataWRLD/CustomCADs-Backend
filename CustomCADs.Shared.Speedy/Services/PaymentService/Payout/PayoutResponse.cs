namespace CustomCADs.Shared.Speedy.Services.PaymentService.Payout;

using Dtos.Payout;

public record PayoutResponse(
    PayoutDto[] Payouts,
    ErrorDto? Error
);
