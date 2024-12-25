using CustomCADs.Carts.Application.Carts.Commands.Create;
using CustomCADs.Carts.Application.Carts.Queries.CalculateShipment;
using CustomCADs.Carts.Application.Carts.Queries.GetAll;
using CustomCADs.Carts.Application.Carts.Queries.GetById;
using CustomCADs.Carts.Application.Common.Dtos;
using CustomCADs.Carts.Domain.Carts;
using CustomCADs.Carts.Domain.Carts.Entities;
using CustomCADs.Shared.Application.Delivery.Dtos;

namespace CustomCADs.Carts.Application.Carts;

internal static class Mapper
{
    internal static GetAllCartsDto ToGetAllCartsItem(this Cart cart, string timeZone)
        => new(
            Id: cart.Id,
            Total: cart.TotalCost,
            PurchaseDate: TimeZoneInfo.ConvertTimeFromUtc(
                cart.PurchaseDate,
                TimeZoneInfo.FindSystemTimeZoneById(timeZone)
            ),
            ItemsCount: cart.Items.Count
        );

    internal static GetCartByIdDto ToGetCartByIdDto(this Cart cart, string timeZone)
        => new(
            Id: cart.Id,
            Total: cart.TotalCost,
            PurchaseDate: TimeZoneInfo.ConvertTimeFromUtc(
                cart.PurchaseDate,
                TimeZoneInfo.FindSystemTimeZoneById(timeZone)
            ),
            BuyerId: cart.BuyerId,
            ShipmentId: cart.ShipmentId,
            Items: [.. cart.Items.Select(i => i.ToCartItemDto())]
        );

    internal static CalculateCartShipmentDto ToCalculateCartShipmentDto(this CalculationDto calculation, string timeZone)
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

    internal static Cart ToCart(this CreateCartCommand command)
        => Cart.Create(command.BuyerId);

    internal static CartItemDto ToCartItemDto(this CartItem item)
        => new(
            Id: item.Id,
            Quantity: item.Quantity,
            Delivery: item.Delivery,
            Weight: item.Weight,
            Price: item.Price,
            Cost: item.Cost,
            ProductId: item.ProductId,
            CartId: item.CartId,
            CadId: item.CadId
        );
}
