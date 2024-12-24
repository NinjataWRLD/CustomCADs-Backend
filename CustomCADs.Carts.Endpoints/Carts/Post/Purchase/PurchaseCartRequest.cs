namespace CustomCADs.Carts.Endpoints.Carts.Post.Purchase;

public sealed record PurchaseCartRequest(
    Guid CartId,
    string PaymentMethodId
);
