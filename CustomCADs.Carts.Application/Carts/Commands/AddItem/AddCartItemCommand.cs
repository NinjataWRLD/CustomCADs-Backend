using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Application.Carts.Commands.AddItem;

public sealed record AddCartItemCommand(
    CartId Id,
    bool Delivery,
    double Weight,
    int Quantity,
    ProductId ProductId,
    AccountId BuyerId
) : ICommand<CartItemId>;
