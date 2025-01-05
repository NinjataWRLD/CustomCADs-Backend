using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.Carts.Application.Common.Dtos;

public record PurchasedCartItemDto(
    PurchasedCartItemId Id,
    int Quantity,
    bool Delivery,
    decimal Price,
    decimal Cost,
    ProductId ProductId,
    PurchasedCartId CartId,
    CadId CadId
);