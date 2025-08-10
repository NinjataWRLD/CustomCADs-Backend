using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Domain.TypedIds.Files;
using CustomCADs.Shared.Domain.TypedIds.Printing;

namespace CustomCADs.Carts.Domain.PurchasedCarts.ValueObjects;

public record CartItemDto(
	decimal Price,
	CadId CadId,
	ProductId ProductId,
	bool ForDelivery,
	CustomizationId? CustomizationId,
	int Quantity,
	DateTimeOffset AddedAt
);
