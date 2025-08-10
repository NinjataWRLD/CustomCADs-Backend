using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Quantity.Increment;

public record IncreaseActiveCartItemQuantityCommand(
	AccountId BuyerId,
	ProductId ProductId,
	int Amount
) : ICommand<int>;
