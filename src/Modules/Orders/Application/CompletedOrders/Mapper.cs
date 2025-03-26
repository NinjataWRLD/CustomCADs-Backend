using CustomCADs.Orders.Application.CompletedOrders.Commands.Create;
using CustomCADs.Orders.Application.CompletedOrders.Queries.ClientGetById;
using CustomCADs.Orders.Application.CompletedOrders.Queries.DesignerGetById;
using CustomCADs.Orders.Application.CompletedOrders.Queries.GetAll;

namespace CustomCADs.Orders.Application.CompletedOrders;

internal static class Mapper
{
    internal static GetAllCompletedOrdersDto ToGetAllDto(this CompletedOrder order, string buyerUsername, string? designerUsername, string timeZone)
        => new(
            Id: order.Id,
            Name: order.Name,
            OrderDate: TimeZoneInfo.ConvertTimeFromUtc(
                order.OrderDate,
                TimeZoneInfo.FindSystemTimeZoneById(timeZone)
            ),
            PurchaseDate: TimeZoneInfo.ConvertTimeFromUtc(
                order.PurchaseDate,
                TimeZoneInfo.FindSystemTimeZoneById(timeZone)
            ),
            Delivery: order.Delivery,
            BuyerName: buyerUsername,
            DesignerName: designerUsername
        );

    internal static ClientGetCompletedOrderByIdDto ToClientGetByIdDto(this CompletedOrder order, string timeZone, string designer)
        => new(
            Id: order.Id,
            Name: order.Name,
            Description: order.Description,
            OrderDate: TimeZoneInfo.ConvertTimeFromUtc(
                order.OrderDate,
                TimeZoneInfo.FindSystemTimeZoneById(timeZone)
            ),
            PurchaseDate: TimeZoneInfo.ConvertTimeFromUtc(
                order.PurchaseDate,
                TimeZoneInfo.FindSystemTimeZoneById(timeZone)
            ),
            Delivery: order.Delivery,
            DesignerName: designer,
            ShipmentId: order.ShipmentId
        );

    internal static DesignerGetCompletedOrderByIdDto ToDesignerGetByIdDto(this CompletedOrder order, string buyer)
        => new(
            Id: order.Id,
            Name: order.Name,
            Description: order.Description,
            OrderDate: order.OrderDate,
            PurchaseDate: order.PurchaseDate,
            Delivery: order.Delivery,
            BuyerName: buyer,
            ShipmentId: order.ShipmentId
        );

    internal static CompletedOrder ToEntity(this CreateCompletedOrderCommand order)
        => CompletedOrder.Create(
            name: order.Name,
            description: order.Description,
            price: order.Price,
            delivery: order.Delivery,
            orderDate: order.OrderDate,
            buyerId: order.BuyerId,
            designerId: order.DesignerId,
            cadId: order.CadId
        );
}
