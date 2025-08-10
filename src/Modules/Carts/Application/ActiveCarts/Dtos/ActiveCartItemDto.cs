using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Domain.TypedIds.Printing;

namespace CustomCADs.Carts.Application.ActiveCarts.Dtos;

public record ActiveCartItemDto(
	int Quantity,
	bool ForDelivery,
	string BuyerName,
	DateTimeOffset AddedAt,
	AccountId BuyerId,
	ProductId ProductId,
	CustomizationId? CustomizationId
);
