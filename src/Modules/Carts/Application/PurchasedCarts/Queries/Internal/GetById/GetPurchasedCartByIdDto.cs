using CustomCADs.Shared.Core.Common.TypedIds.Delivery;

namespace CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetById;

public record GetPurchasedCartByIdDto(
	PurchasedCartId Id,
	decimal Total,
	DateTimeOffset PurchasedAt,
	string BuyerName,
	ShipmentId? ShipmentId,
	ICollection<PurchasedCartItemDto> Items
);
