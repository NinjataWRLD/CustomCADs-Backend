using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Domain.TypedIds.Printing;

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
