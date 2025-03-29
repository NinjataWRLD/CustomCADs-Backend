using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Quantity.Decrement;

public record DecreaseActiveCartItemQuantityCommand(
    AccountId BuyerId,
    ProductId ProductId,
    int Amount
) : ICommand<int>;
