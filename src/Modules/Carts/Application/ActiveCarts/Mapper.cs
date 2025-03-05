using CustomCADs.Carts.Application.ActiveCarts.Commands.Create;
using CustomCADs.Carts.Application.ActiveCarts.Queries.CalculateShipment;
using CustomCADs.Carts.Application.ActiveCarts.Queries.GetByBuyerId;
using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Shared.Abstractions.Delivery.Dtos;

namespace CustomCADs.Carts.Application.ActiveCarts;

internal static class Mapper
{
    internal static GetActiveCartDto ToGetCartByIdDto(this ActiveCart cart, string buyer)
        => new(
            Id: cart.Id,
            BuyerName: buyer,
            Items: [.. cart.Items.Select(i => i.ToCartItemDto())]
        );

    internal static CalculateActiveCartShipmentDto ToCalculateCartShipmentDto(this CalculationDto calculation, string timeZone)
        => new(
            Total: calculation.Price.Total,
            Currency: calculation.Price.Currency,
            PickupDate: DateOnly.FromDateTime(TimeZoneInfo.ConvertTime(
                calculation.PickupDate.ToDateTime(new TimeOnly(9, 0)),
                TimeZoneInfo.FindSystemTimeZoneById(timeZone)
            )),
            DeliveryDeadline: TimeZoneInfo.ConvertTime(
                calculation.DeliveryDeadline,
                TimeZoneInfo.FindSystemTimeZoneById(timeZone)
            ),
            Service: calculation.Service
        );

    internal static ActiveCart ToCart(this CreateActiveCartCommand command)
        => ActiveCart.Create(command.BuyerId);

    internal static ActiveCartItemDto ToCartItemDto(this ActiveCartItem item)
        => new(
            Quantity: item.Quantity,
            ForDelivery: item.ForDelivery,
            ProductId: item.ProductId,
            CartId: item.CartId,
            CustomizationId: item.CustomizationId
        );
}
