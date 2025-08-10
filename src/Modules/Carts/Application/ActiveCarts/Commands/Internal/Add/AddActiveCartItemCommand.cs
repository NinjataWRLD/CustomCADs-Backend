using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Domain.TypedIds.Printing;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Add;

public sealed record AddActiveCartItemCommand(
	AccountId BuyerId,
	bool ForDelivery,
	ProductId ProductId,
	CustomizationId? CustomizationId
) : ICommand;
