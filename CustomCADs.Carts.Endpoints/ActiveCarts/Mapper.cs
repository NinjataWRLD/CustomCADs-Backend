using CustomCADs.Carts.Application.ActiveCarts.Queries.CalculateShipment;
using CustomCADs.Carts.Application.ActiveCarts.Queries.GetById;
using CustomCADs.Carts.Endpoints.ActiveCarts.Get.CalculateShipment;
using CustomCADs.Carts.Endpoints.ActiveCarts.Get.Single;
using CustomCADs.Carts.Endpoints.ActiveCarts.Post.Cart;

namespace CustomCADs.Carts.Endpoints.ActiveCarts;

using static Constants;

internal static class Mapper
{
    internal static GetActiveCartResponse ToGetCartResponse(this GetActiveCartDto cart)
        => new(
            Id: cart.Id.Value,
            BuyerId: cart.BuyerId.Value,
            Items: [.. cart.Items.Select(o => o.ToCartItemResponse())]
        );

    internal static PostActiveCartResponse ToPostCartResponse(this GetActiveCartDto cart)
        => new(
            Id: cart.Id.Value,
            BuyerId: cart.BuyerId.Value
        );

    internal static ActiveCartItemResponse ToCartItemResponse(this ActiveCartItemDto item)
        => new(
            Id: item.Id.Value,
            Quantity: item.Quantity,
            Delivery: item.Delivery,
            Weight: item.Weight,
            ProductId: item.ProductId.Value,
            CartId: item.CartId.Value
        );

    internal static CalculateActiveCartShipmentResponse ToCalculateCartShipmentResponse(this CalculateActiveCartShipmentDto calculation)
        => new(
            Service: calculation.Service,
            Total: calculation.Total,
            Currency: calculation.Currency,
            PickupDate: calculation.PickupDate.ToString(SpeedyDateFormatString),
            DeliveryDeadline: calculation.DeliveryDeadline.ToString(SpeedyDateFormatString)
        );
}
