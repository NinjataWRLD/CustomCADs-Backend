using CustomCADs.Carts.Application.PurchasedCarts.Dtos;
using CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetAll;
using CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetById;
using CustomCADs.Carts.Endpoints.PurchasedCarts.Dtos;
using CustomCADs.Carts.Endpoints.PurchasedCarts.Endpoints.Get.All;
using CustomCADs.Carts.Endpoints.PurchasedCarts.Endpoints.Get.Single;

namespace CustomCADs.Carts.Endpoints.PurchasedCarts;

using static Constants;

internal static class Mapper
{
    internal static GetPurchasedCartsResponse ToResponse(this GetAllPurchasedCartsDto cart)
        => new(
            Id: cart.Id.Value,
            Total: cart.Total,
            PurchaseDate: cart.PurchaseDate.ToString(DateFormatString),
            ItemsCount: cart.ItemsCount
        );

    internal static GetPurchasedCartResponse ToResponse(this GetPurchasedCartByIdDto cart)
        => new(
            Id: cart.Id.Value,
            Total: cart.Total,
            PurchaseDate: cart.PurchaseDate.ToString(DateFormatString),
            BuyerName: cart.BuyerName,
            ShipmentId: cart.ShipmentId?.Value,
            Items: [.. cart.Items.Select(o => o.ToResponse())]
        );

    internal static PurchasedCartItemResponse ToResponse(this PurchasedCartItemDto item)
        => new(
            Quantity: item.Quantity,
            ForDelivery: item.ForDelivery,
            Price: item.Price,
            Cost: item.Cost,
            ProductId: item.ProductId.Value,
            CartId: item.CartId.Value,
            CustomizationId: item.CustomizationId?.Value
        );
}
