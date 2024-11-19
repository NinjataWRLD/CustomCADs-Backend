using CustomCADs.Orders.Application.Carts.Queries.GetAll;
using CustomCADs.Orders.Application.Carts.Queries.GetById;
using CustomCADs.Orders.Domain.Carts.Entities;
using CustomCADs.Orders.Endpoints.Carts.Get;
using CustomCADs.Orders.Endpoints.Carts.GetAll;
using CustomCADs.Orders.Endpoints.Carts.Recent;

namespace CustomCADs.Orders.Endpoints.Carts;

using static Constants;

public static class Mapper
{
    public static GetCartsDto ToGetCartsDto(this GetAllCartsItem item)
        => new(
            Id: item.Id.Value,
            Total: item.Total,
            PurchaseDate: item.PurchaseDate.ToString(DateFormatString),
            OrdersCount: item.OrdersCount
        );
    
    public static RecentCartsResponse ToRecentCartsResponse(this GetAllCartsItem item)
        => new(
            Id: item.Id.Value,
            PurchaseDate: item.PurchaseDate.ToString(DateFormatString)
        );

    public static GetCartResponse ToGetCartResponse(this GetCartByIdDto dto)
        => new(
            Id: dto.Id.Value,
            Total: dto.Total,
            PurchaseDate: dto.PurchaseDate.ToString(DateFormatString),
            BuyerId: dto.BuyerId.Value,
            Orders: [.. dto.Orders.Select(o => o.ToGalleryOrderDto())]
        );

    public static GalleryOrderDto ToGalleryOrderDto(this GalleryOrder order)
        => new(
            Id: order.Id.Value,
            Quantity: order.Quantity,
            DeliveryType: order.DeliveryType.ToString(),
            Price: new(order.Price),
            PurchaseDate: order.PurchaseDate.ToString(DateFormatString),
            ProductId: order.ProductId.Value,
            CartId: order.CartId.Value,
            CadId: order.CadId is null ? null : order.CadId.Value!.Value,
            ShipmentId: order.ShipmentId is null ? null : order.ShipmentId.Value!.Value,
            Cost: new(order.Cost)
        );
}
