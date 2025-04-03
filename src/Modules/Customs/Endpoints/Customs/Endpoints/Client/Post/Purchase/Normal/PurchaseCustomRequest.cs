namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Client.Post.Purchase.Normal;

public sealed record PurchaseCustomRequest(
    Guid Id,
    string PaymentMethodId
);
