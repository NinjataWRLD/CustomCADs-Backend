using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Application.Common.Dtos;

public record ActiveCartItemDto(
    int Quantity,
    bool ForDelivery,
    double Weight,
    ProductId ProductId,
    ActiveCartId CartId
);