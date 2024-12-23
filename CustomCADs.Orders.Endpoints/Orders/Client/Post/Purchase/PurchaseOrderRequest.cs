namespace CustomCADs.Orders.Endpoints.Orders.Client.Post.Purchase;

public sealed record PurchaseOrderRequest(
    Guid OrderId,
    string PaymentMethodId
);
