using CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetAll;
using CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetById;
using CustomCADs.Carts.Domain.PurchasedCarts.Entities;
using CustomCADs.Shared.Core.Extensions;

namespace CustomCADs.Carts.Application.PurchasedCarts;

internal static class Mapper
{
    internal static GetAllPurchasedCartsDto ToGetAllDto(this PurchasedCart cart, string timeZone)
        => new(
            Id: cart.Id,
            Total: cart.TotalCost,
            PurchasedAt: cart.PurchasedAt.ToUserLocalTime(timeZone),
            ItemsCount: cart.Items.Count
        );

    internal static GetPurchasedCartByIdDto ToGetByIdDto(this PurchasedCart cart, string timeZone, string buyer)
        => new(
            Id: cart.Id,
            Total: cart.TotalCost,
            PurchasedAt: cart.PurchasedAt.ToUserLocalTime(timeZone),
            BuyerName: buyer,
            ShipmentId: cart.ShipmentId,
            Items: [.. cart.Items.Select(i => i.ToDto())]
        );

    internal static PurchasedCartItemDto ToDto(this PurchasedCartItem item)
        => new(
            Quantity: item.Quantity,
            ForDelivery: item.ForDelivery,
            Price: item.Price,
            Cost: item.Cost,
            ProductId: item.ProductId,
            CartId: item.CartId,
            CustomizationId: item.CustomizationId
        );
}
