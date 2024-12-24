using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Application.Carts.Commands.AddItem;

public sealed record AddCartItemCommand(
    CartId Id,
    int Quantity,
    double Weight,
    ProductId ProductId,
    AccountId BuyerId
) : ICommand<CartItemId>;
