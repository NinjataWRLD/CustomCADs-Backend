using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;

namespace CustomCADs.Carts.Application.ActiveCarts.Dtos;

public record ActiveCartItemDto(
    int Quantity,
    bool ForDelivery,
    ProductId ProductId,
    ActiveCartId CartId,
    CustomizationId? CustomizationId
);