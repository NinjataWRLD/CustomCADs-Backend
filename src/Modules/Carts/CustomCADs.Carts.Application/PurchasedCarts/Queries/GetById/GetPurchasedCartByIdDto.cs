using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;

namespace CustomCADs.Carts.Application.PurchasedCarts.Queries.GetById;

public record GetPurchasedCartByIdDto(
    PurchasedCartId Id,
    decimal Total,
    DateTime PurchaseDate,
    AccountId BuyerId,
    ShipmentId? ShipmentId,
    ICollection<PurchasedCartItemDto> Items
);
