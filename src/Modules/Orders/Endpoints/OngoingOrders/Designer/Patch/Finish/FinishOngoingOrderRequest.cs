namespace CustomCADs.Orders.Endpoints.OngoingOrders.Designer.Patch.Finish;

public sealed record FinishOngoingOrderRequest(
    Guid Id,
    decimal Price,
    string CadKey,
    string CadContentType
);