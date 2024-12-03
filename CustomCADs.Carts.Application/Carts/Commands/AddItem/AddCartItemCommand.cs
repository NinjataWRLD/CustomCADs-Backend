using CustomCADs.Carts.Domain.Carts.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Inventory;

namespace CustomCADs.Carts.Application.Carts.Commands.AddItem;

public sealed record AddCartItemCommand(
    CartId Id,
    DeliveryType DeliveryType,
    int Quantity,
    ProductId ProductId,
    AccountId BuyerId
) : ICommand<CartItemId>;
