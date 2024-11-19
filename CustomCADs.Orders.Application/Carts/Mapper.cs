using CustomCADs.Orders.Application.Carts.Commands.Create;
using CustomCADs.Orders.Application.Carts.Queries.GetAll;
using CustomCADs.Orders.Application.Carts.Queries.GetById;
using CustomCADs.Orders.Application.Carts.Queries.GetOrders;
using CustomCADs.Orders.Domain.Carts.Entities;

namespace CustomCADs.Orders.Application.Carts;

public static class Mapper
{
    public static GetAllCartsItem ToGetAllCartsItem(this Cart cart)
        => new(
            Id: cart.Id,
            Total: cart.Total,
            PurchaseDate: cart.PurchaseDate,
            OrdersCount: cart.Orders.Count
        );

    public static GetCartByIdDto ToGetCartByIdDto(this Cart cart)
        => new(
            Id: cart.Id,
            Total: cart.Total,
            PurchaseDate: cart.PurchaseDate,
            BuyerId: cart.BuyerId,
            Orders: [.. cart.Orders]
        );

    public static Cart ToCart(this CreateCartCommand req)
        => Cart.Create(req.BuyerId);
    
    public static GetCartOrdersByIdDto ToGetCartOrdersByIdDto(this GalleryOrder order)
        => new(
            Id: order.Id,
            Quantity: order.Quantity,
            DeliveryType: order.DeliveryType,
            Price: order.Price,
            Cost: order.Cost,
            PurchaseDate: order.PurchaseDate,
            ProductId: order.ProductId,
            CartId: order.CartId,
            CadId: order.CadId,
            ShipmentId: order.ShipmentId
        );
}
