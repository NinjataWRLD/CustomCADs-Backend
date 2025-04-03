using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;

namespace CustomCADs.Carts.Application.PurchasedCarts.Dtos;

public record PurchasedCartItemDto(
    int Quantity,
    bool ForDelivery,
    decimal Price,
    decimal Cost,
    DateTimeOffset AddedAt,
    ProductId ProductId,
    PurchasedCartId CartId,
    CustomizationId? CustomizationId
);