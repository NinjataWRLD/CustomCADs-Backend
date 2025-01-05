using CustomCADs.Carts.Application.ActiveCarts.Queries.CalculateShipment;
using CustomCADs.Carts.Application.PurchasedCarts.Queries.GetAll;
using CustomCADs.Carts.Application.PurchasedCarts.Queries.GetById;
using CustomCADs.Carts.Domain.PurchasedCarts.Entities;
using CustomCADs.Shared.Application.Delivery.Dtos;

namespace CustomCADs.Carts.Application.PurchasedCarts;

internal static class Mapper
{
    internal static GetAllPurchasedCartsDto ToGetAllCartsItem(this PurchasedCart cart, string timeZone)
        => new(
            Id: cart.Id,
            Total: cart.TotalCost,
            PurchaseDate: TimeZoneInfo.ConvertTimeFromUtc(
                cart.PurchaseDate,
                TimeZoneInfo.FindSystemTimeZoneById(timeZone)
            ),
            ItemsCount: cart.Items.Count
        );

    internal static GetPurchasedCartByIdDto ToGetCartByIdDto(this PurchasedCart cart, string timeZone)
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

    internal static PurchasedCartItemDto ToCartItemDto(this PurchasedCartItem item)
        => new(
            Id: item.Id,
            Quantity: item.Quantity,
            Delivery: item.Delivery,
            Price: item.Price,
            Cost: item.Cost,
            ProductId: item.ProductId,
            CartId: item.CartId,
            CadId: item.CadId
        );
}
