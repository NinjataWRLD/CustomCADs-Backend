using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.Core.Common.TypedIds.Printing;

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
