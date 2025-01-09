namespace CustomCADs.Orders.Endpoints.OngoingOrders.Designer.Patch.Finish;

public record FinishOngoingOrderResponse(
    string PresignedKey,
    string GeneratedUrl
);