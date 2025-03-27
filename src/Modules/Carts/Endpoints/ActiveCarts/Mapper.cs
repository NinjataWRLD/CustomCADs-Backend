using CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.GetByBuyerId;
using CustomCADs.Carts.Endpoints.ActiveCarts.Endpoints.Get.CalculateShipment;
using CustomCADs.Carts.Endpoints.ActiveCarts.Endpoints.Get.Single;
using CustomCADs.Carts.Endpoints.ActiveCarts.Endpoints.Post.Cart;
using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Carts.Endpoints.ActiveCarts;

using static Constants;

internal static class Mapper
{
    internal static GetActiveCartResponse ToGetResponse(this GetActiveCartDto cart)
        => new(
            Id: cart.Id.Value,
            BuyerName: cart.BuyerName,
            Items: [.. cart.Items.Select(o => o.ToResponse())]
        );

    internal static PostActiveCartResponse ToPostResponse(this GetActiveCartDto cart)
        => new(
            Id: cart.Id.Value,
            BuyerName: cart.BuyerName
        );

    internal static ActiveCartItemResponse ToResponse(this ActiveCartItemDto item)
        => new(
            Quantity: item.Quantity,
            ForDelivery: item.ForDelivery,
            ProductId: item.ProductId.Value,
            CartId: item.CartId.Value,
            CustomizationId: item.CustomizationId?.Value
        );

    internal static CalculateActiveCartShipmentResponse ToResponse(this CalculateShipmentDto calculation)
        => new(
            Service: calculation.Service,
            Total: calculation.Total,
            Currency: calculation.Currency,
            PickupDate: calculation.PickupDate.ToString(SpeedyDateFormatString),
            DeliveryDeadline: calculation.DeliveryDeadline.ToString(SpeedyDateFormatString)
        );
}
