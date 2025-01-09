namespace CustomCADs.Orders.Endpoints.OngoingOrders.Client.Post.Purchase.Normal;

public sealed record PurchaseOngoingOrderRequest(
    Guid Id,
    string PaymentMethodId
);
