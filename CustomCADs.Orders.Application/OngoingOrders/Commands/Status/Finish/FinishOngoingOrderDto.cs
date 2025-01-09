namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Status.Finish;

public record FinishOngoingOrderDto(
    string PresignedKey,
    string GeneratedUrl
);
