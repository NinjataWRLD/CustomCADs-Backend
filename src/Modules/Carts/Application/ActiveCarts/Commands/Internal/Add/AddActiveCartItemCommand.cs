using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Printing;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Add;

public sealed record AddActiveCartItemCommand(
	AccountId BuyerId,
	bool ForDelivery,
	ProductId ProductId,
	CustomizationId? CustomizationId
) : ICommand;
