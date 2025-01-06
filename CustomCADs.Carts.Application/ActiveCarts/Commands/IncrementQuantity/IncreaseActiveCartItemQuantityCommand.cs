using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.IncrementQuantity;

public record IncreaseActiveCartItemQuantityCommand(
    AccountId BuyerId,
    ActiveCartItemId ItemId,
    int Amount
) : ICommand<int>;
