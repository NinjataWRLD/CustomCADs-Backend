using CustomCADs.Carts.Application.ActiveCarts.Commands.Create;
using CustomCADs.Carts.Application.ActiveCarts.Queries.CalculateShipment;
using CustomCADs.Carts.Application.ActiveCarts.Queries.GetById;
using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Shared.Application.Delivery.Dtos;

namespace CustomCADs.Carts.Application.ActiveCarts;

internal static class Mapper
{
    internal static GetActiveCartDto ToGetCartByIdDto(this ActiveCart cart, string timeZone)
        => new(
            Id: cart.Id,
            BuyerId: cart.BuyerId,
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
            Id: item.Id,
            Quantity: item.Quantity,
            Delivery: item.Delivery,
            Weight: item.Weight,
            ProductId: item.ProductId,
            CartId: item.CartId
        );
}
