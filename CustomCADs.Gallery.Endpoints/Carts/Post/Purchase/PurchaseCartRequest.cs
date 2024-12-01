namespace CustomCADs.Gallery.Endpoints.Carts.Post.Purchase;

public record PurchaseCartRequest(
    Guid CartId,
    string PaymentMethodId
);
