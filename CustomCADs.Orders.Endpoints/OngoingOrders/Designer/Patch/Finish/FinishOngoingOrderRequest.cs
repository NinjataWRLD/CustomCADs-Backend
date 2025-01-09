namespace CustomCADs.Orders.Endpoints.OngoingOrders.Designer.Patch.Finish;

public sealed record FinishOngoingOrderRequest(
    Guid Id,
    string CadKey,
    string CadContentType
);