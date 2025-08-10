using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Remove;

public sealed record RemoveActiveCartItemCommand(
	AccountId BuyerId,
	ProductId ProductId
) : ICommand;
