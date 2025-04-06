namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customs.Post.Purchase.Normal;

public sealed record PurchaseCustomRequest(
    Guid Id,
    string PaymentMethodId
);
