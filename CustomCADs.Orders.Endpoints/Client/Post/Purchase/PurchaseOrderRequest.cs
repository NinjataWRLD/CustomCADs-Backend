namespace CustomCADs.Orders.Endpoints.Client.Post.Purchase;

public record PurchaseOrderRequest(
    Guid OrderId,
    string PaymentMethodId
);
