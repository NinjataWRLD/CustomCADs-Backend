using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Item.Quantity.Decrement;

public record DecreaseActiveCartItemQuantityCommand(
    AccountId BuyerId,
    ActiveCartItemId ItemId,
    int Amount
) : ICommand<int>;
