namespace CustomCADs.Orders.Endpoints.Client.Post.Purchase;

public sealed record PurchaseOrderRequest(
    Guid OrderId,
    string PaymentMethodId
);
