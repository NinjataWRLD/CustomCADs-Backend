using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Item.Remove;

public sealed record RemoveActiveCartItemCommand(
    AccountId BuyerId,
    ActiveCartItemId ItemId
) : ICommand;
