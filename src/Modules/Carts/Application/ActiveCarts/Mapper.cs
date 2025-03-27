using CustomCADs.Carts.Application.ActiveCarts.Dtos;
using CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.GetByBuyerId;
using CustomCADs.Carts.Domain.ActiveCarts.Entities;

namespace CustomCADs.Carts.Application.ActiveCarts;

internal static class Mapper
{
    internal static GetActiveCartDto ToDto(this ActiveCart cart, string buyer)
        => new(
            Id: cart.Id,
            BuyerName: buyer,
            Items: [.. cart.Items.Select(i => i.ToDto())]
        );

    internal static ActiveCartItemDto ToDto(this ActiveCartItem item)
        => new(
            Quantity: item.Quantity,
            ForDelivery: item.ForDelivery,
            ProductId: item.ProductId,
            CartId: item.CartId,
            CustomizationId: item.CustomizationId
        );

    internal static ActiveCartItem ToEntity(this ActiveCartItemDto item)
        => item.CustomizationId is null
        ? ActiveCartItem.Create(
            productId: item.ProductId,
            cartId: item.CartId
        ).IncreaseQuantity(item.Quantity)
        : ActiveCartItem.Create(
            productId: item.ProductId,
            cartId: item.CartId,
            customizationId: item.CustomizationId.Value
        ).IncreaseQuantity(item.Quantity);
}
