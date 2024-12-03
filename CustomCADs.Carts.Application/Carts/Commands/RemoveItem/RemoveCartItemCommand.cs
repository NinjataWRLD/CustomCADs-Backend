using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.Carts.Commands.RemoveItem;

public sealed record RemoveCartItemCommand(
    CartId Id,
    CartItemId ItemId,
    AccountId BuyerId
) : ICommand;
