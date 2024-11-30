using CustomCADs.Gallery.Application.Carts.Commands.Create;
using CustomCADs.Gallery.Application.Carts.Queries.GetAll;
using CustomCADs.Gallery.Application.Carts.Queries.GetById;
using CustomCADs.Gallery.Application.Carts.Queries.GetItems;
using CustomCADs.Gallery.Domain.Carts;
using CustomCADs.Gallery.Domain.Carts.Entities;
using System;

namespace CustomCADs.Gallery.Application.Carts;

public static class Mapper
{
    public static GetAllCartsDto ToGetAllCartsItem(this Cart cart, string timeZone)
        => new(
            Id: cart.Id,
            Total: cart.Total,
            PurchaseDate: TimeZoneInfo.ConvertTimeToUtc(
                cart.PurchaseDate,
                TimeZoneInfo.FindSystemTimeZoneById(timeZone)
            ),
            ItemsCount: cart.Items.Count
        );

    public static GetCartByIdDto ToGetCartByIdDto(this Cart cart, string timeZone)
        => new(
            Id: cart.Id,
            Total: cart.Total,
            PurchaseDate: TimeZoneInfo.ConvertTimeToUtc(
                cart.PurchaseDate, 
                TimeZoneInfo.FindSystemTimeZoneById(timeZone)
            ),
            BuyerId: cart.BuyerId,
            Items: [.. cart.Items]
        );

    public static Cart ToCart(this CreateCartCommand command)
        => Cart.Create(command.BuyerId);

    public static GetCartItemsByIdDto ToGetCartItemsByIdDto(this CartItem item)
        => new(
            Id: item.Id,
            Quantity: item.Quantity,
            DeliveryType: item.DeliveryType,
            Price: item.Price,
            Cost: item.Cost,
            PurchaseDate: item.PurchaseDate,
            ProductId: item.ProductId,
            CartId: item.CartId,
            CadId: item.CadId,
            ShipmentId: item.ShipmentId
        );
}
