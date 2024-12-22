using CustomCADs.Carts.Application.Common.Dtos;
using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Carts.Endpoints.Carts.Post.Purchase;

public sealed record PurchaseCartRequest(
    Guid CartId,
    AddressDto Address,
    ContactDto Contact,
    string PaymentMethodId
);
