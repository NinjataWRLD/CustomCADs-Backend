namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Client.Post.Purchase.Normal;

public sealed record PurchaseOngoingOrderRequest(
    Guid Id,
    string PaymentMethodId
);
