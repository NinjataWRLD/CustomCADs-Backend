namespace CustomCADs.Shared.Speedy.API.Endpoints.PaymentEndpoints.Payout;

using Dtos.Payout;

public record PayoutResponse(
    PayoutDto[] Payouts,
    ErrorDto? Error
);
