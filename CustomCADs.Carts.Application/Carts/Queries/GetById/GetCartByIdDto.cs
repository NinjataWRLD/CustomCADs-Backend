using CustomCADs.Carts.Application.Common.Dtos;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;

namespace CustomCADs.Carts.Application.Carts.Queries.GetById;

public record GetCartByIdDto(
    CartId Id,
    decimal Total,
    DateTime PurchaseDate,
    AccountId BuyerId,
    ShipmentId? ShipmentId,
    ICollection<CartItemDto> Items
);
