using CustomCADs.Gallery.Application.Carts.Queries.GetAll;
using CustomCADs.Gallery.Application.Carts.Queries.GetById;
using CustomCADs.Gallery.Domain.Carts.Entities;
using CustomCADs.Gallery.Endpoints.Carts.Get;
using CustomCADs.Gallery.Endpoints.Carts.GetAll;
using CustomCADs.Gallery.Endpoints.Carts.Recent;

namespace CustomCADs.Gallery.Endpoints.Carts;

using static Constants;

public static class Mapper
{
    public static GetCartsDto ToGetCartsDto(this GetAllCartsItem cart)
        => new(
            Id: cart.Id.Value,
            Total: cart.Total,
            PurchaseDate: cart.PurchaseDate.ToString(DateFormatString),
            ItemsCount: cart.ItemsCount
        );

    public static RecentCartsResponse ToRecentCartsResponse(this GetAllCartsItem cart)
        => new(
            Id: cart.Id.Value,
            PurchaseDate: cart.PurchaseDate.ToString(DateFormatString)
        );

    public static GetCartResponse ToGetCartResponse(this GetCartByIdDto cart)
        => new(
            Id: cart.Id.Value,
            Total: cart.Total,
            PurchaseDate: cart.PurchaseDate.ToString(DateFormatString),
            BuyerId: cart.BuyerId.Value,
            Items: [.. cart.Items.Select(o => o.ToCartItemDto())]
        );

    public static CartItemDto ToCartItemDto(this CartItem item)
        => new(
            Id: item.Id.Value,
            Quantity: item.Quantity,
            DeliveryType: item.DeliveryType.ToString(),
            Price: new(item.Price),
            PurchaseDate: item.PurchaseDate.ToString(DateFormatString),
            ProductId: item.ProductId.Value,
            CartId: item.CartId.Value,
            CadId: item.CadId is null ? null : item.CadId.Value!.Value,
            ShipmentId: item.ShipmentId is null ? null : item.ShipmentId.Value!.Value,
            Cost: new(item.Cost)
        );
}
