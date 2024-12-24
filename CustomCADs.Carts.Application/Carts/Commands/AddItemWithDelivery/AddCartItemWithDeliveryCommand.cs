using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Application.Carts.Commands.AddItemWithDelivery;

public sealed record AddCartItemWithDeliveryCommand(
    CartId Id,
    int Quantity,
    double Weight,
    ProductId ProductId,
    AccountId BuyerId
) : ICommand<CartItemId>;
