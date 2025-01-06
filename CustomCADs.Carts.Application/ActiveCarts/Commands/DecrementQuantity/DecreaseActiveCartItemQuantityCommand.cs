using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.DecrementQuantity;

public record DecreaseActiveCartItemQuantityCommand(
    AccountId BuyerId,
    ActiveCartItemId ItemId,
    int Amount
) : ICommand<int>;
