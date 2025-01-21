using CustomCADs.Carts.Application.ActiveCarts.Queries.CalculateShipment;
using CustomCADs.Carts.Application.PurchasedCarts.Queries.GetAll;
using CustomCADs.Carts.Application.PurchasedCarts.Queries.GetById;
using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Carts.Domain.PurchasedCarts.Entities;
using CustomCADs.Shared.Abstractions.Delivery.Dtos;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

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

    internal static GetPurchasedCartByIdDto ToGetCartByIdDto(this PurchasedCart cart, string timeZone, string buyer)
        => new(
            Id: cart.Id,
            Total: cart.TotalCost,
            PurchaseDate: TimeZoneInfo.ConvertTimeFromUtc(
                cart.PurchaseDate,
                TimeZoneInfo.FindSystemTimeZoneById(timeZone)
            ),
            BuyerName: buyer,
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
            ForDelivery: item.ForDelivery,
            Price: item.Price,
            Cost: item.Cost,
            ProductId: item.ProductId,
            CartId: item.CartId
        );

    internal static (decimal Price, CadId CadId, ActiveCartItem Item) ToPurchasedCartItemDto(this ActiveCartItem item, Dictionary<ProductId, decimal> prices, Dictionary<ProductId, CadId> productCads, Dictionary<CadId, CadId> itemCads)
    {
        decimal price = prices[item.ProductId];
        CadId productCadId = productCads[item.ProductId];
        CadId itemCadId = itemCads[productCadId];

        return (price, itemCadId, item);
    }
}
