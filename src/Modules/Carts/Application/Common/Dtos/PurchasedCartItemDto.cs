using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;

namespace CustomCADs.Carts.Application.Common.Dtos;

public record PurchasedCartItemDto(
    int Quantity,
    bool ForDelivery,
    decimal Price,
    decimal Cost,
    ProductId ProductId,
    PurchasedCartId CartId,
    CustomizationId? CustomizationId
);