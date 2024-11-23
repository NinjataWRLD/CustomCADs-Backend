using CustomCADs.Gallery.Domain.Carts.Enums;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Inventory;

namespace CustomCADs.Gallery.Application.Carts.Commands.AddItem;

public record AddCartItemCommand(
    CartId Id,
    DeliveryType DeliveryType,
    int Quantity,
    ProductId ProductId
) : ICommand<CartItemId>;
