using CustomCADs.Carts.Application.Carts.Commands.Create;
using CustomCADs.Carts.Application.Carts.Queries.GetAll;
using CustomCADs.Carts.Application.Carts.Queries.GetById;
using CustomCADs.Carts.Application.Common.Dtos;
using CustomCADs.Carts.Domain.Carts;
using CustomCADs.Carts.Domain.Carts.Entities;

namespace CustomCADs.Carts.Application.Carts;

internal static class Mapper
{
    internal static GetAllCartsDto ToGetAllCartsItem(this Cart cart, string timeZone)
        => new(
            Id: cart.Id,
            Total: cart.Total,
            PurchaseDate: TimeZoneInfo.ConvertTimeToUtc(
                cart.PurchaseDate,
                TimeZoneInfo.FindSystemTimeZoneById(timeZone)
            ),
            ItemsCount: cart.Items.Count
        );

    internal static GetCartByIdDto ToGetCartByIdDto(this Cart cart, string timeZone)
        => new(
            Id: cart.Id,
            Total: cart.Total,
            PurchaseDate: TimeZoneInfo.ConvertTimeToUtc(
                cart.PurchaseDate,
                TimeZoneInfo.FindSystemTimeZoneById(timeZone)
            ),
            BuyerId: cart.BuyerId,
            ShipmentId: cart.ShipmentId,
            Items: [.. cart.Items.Select(i => i.ToCartItemDto())]
        );

    internal static Cart ToCart(this CreateCartCommand command)
        => Cart.Create(command.BuyerId);

    internal static CartItemDto ToCartItemDto(this CartItem item)
        => new(
            Id: item.Id,
            Quantity: item.Quantity,
            Delivery: item.Delivery,
            Price: item.Price,
            Cost: item.Cost,
            PurchaseDate: item.PurchaseDate,
            ProductId: item.ProductId,
            CartId: item.CartId,
            CadId: item.CadId
        );
}
