using CustomCADs.Carts.Application.PurchasedCarts.Queries.GetAll;
using CustomCADs.Carts.Application.PurchasedCarts.Queries.GetById;
using CustomCADs.Carts.Endpoints.PurchasedCarts.Get.All;
using CustomCADs.Carts.Endpoints.PurchasedCarts.Get.Single;

namespace CustomCADs.Carts.Endpoints.PurchasedCarts;

using static Constants;

internal static class Mapper
{
    internal static GetPurchasedCartsResponse ToGetPurchasedCartsDto(this GetAllPurchasedCartsDto cart)
        => new(
            Id: cart.Id.Value,
            Total: cart.Total,
            PurchaseDate: cart.PurchaseDate.ToString(DateFormatString),
            ItemsCount: cart.ItemsCount
        );

    internal static GetPurchasedCartResponse ToGetPurchasedCartResponse(this GetPurchasedCartByIdDto cart)
        => new(
            Id: cart.Id.Value,
            Total: cart.Total,
            PurchaseDate: cart.PurchaseDate.ToString(DateFormatString),
            BuyerId: cart.BuyerId.Value,
            ShipmentId: cart.ShipmentId?.Value,
            Items: [.. cart.Items.Select(o => o.ToPurchasedCartItemResponse())]
        );

    internal static PurchasedCartItemResponse ToPurchasedCartItemResponse(this PurchasedCartItemDto item)
        => new(
            Id: item.Id.Value,
            Quantity: item.Quantity,
            Delivery: item.Delivery,
            Price: item.Price,
            Cost: item.Cost,
            ProductId: item.ProductId.Value,
            CartId: item.CartId.Value,
            CadId: item.CadId.Value
        );
}
