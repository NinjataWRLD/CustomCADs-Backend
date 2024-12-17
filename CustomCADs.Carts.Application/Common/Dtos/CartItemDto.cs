using CustomCADs.Shared.Core.Common.TypedIds.Cads;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Application.Common.Dtos;

public record CartItemDto(
    CartItemId Id,
    int Quantity,
    bool Delivery,
    decimal Price,
    decimal Cost,
    DateTime PurchaseDate,
    ProductId ProductId,
    CartId CartId,
    CadId? CadId
);