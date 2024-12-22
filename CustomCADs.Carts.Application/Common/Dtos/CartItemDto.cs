using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.Carts.Application.Common.Dtos;

public record CartItemDto(
    CartItemId Id,
    int Quantity,
    bool Delivery,
    double Weight,
    decimal Price,
    decimal Cost,
    DateTime PurchaseDate,
    ProductId ProductId,
    CartId CartId,
    CadId? CadId
);