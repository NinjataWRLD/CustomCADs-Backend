using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Application.Common.Dtos;

public record PurchasedCartItemDto(
    PurchasedCartItemId Id,
    int Quantity,
    bool ForDelivery,
    decimal Price,
    decimal Cost,
    ProductId ProductId,
    PurchasedCartId CartId
);