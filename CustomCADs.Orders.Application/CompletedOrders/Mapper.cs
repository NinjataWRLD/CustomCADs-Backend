using CustomCADs.Orders.Application.CompletedOrders.Queries.ClientGetById;
using CustomCADs.Orders.Application.CompletedOrders.Queries.DesignerGetById;
using CustomCADs.Orders.Application.CompletedOrders.Queries.GetAll;

namespace CustomCADs.Orders.Application.CompletedOrders;

internal static class Mapper
{
    internal static GetAllCompletedOrdersDto ToGetAllOrdersItem(this CompletedOrder order, string buyerUsername, string? designerUsername, string timeZone)
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

    internal static ClientGetCompletedOrderByIdDto ToGetOrderByIdDto(this CompletedOrder order, string timeZone)
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
            DesignerId: order.DesignerId,
            CadId: order.CadId,
            ShipmentId: order.ShipmentId
        );

    internal static DesignerGetCompletedOrderByIdDto ToDesignerGetOrderByIdDto(this CompletedOrder order)
        => new(
            Id: order.Id,
            Name: order.Name,
            Description: order.Description,
            OrderDate: order.OrderDate,
            PurchaseDate: order.PurchaseDate,
            Delivery: order.Delivery,
            BuyerId: order.BuyerId,
            CadId: order.CadId,
            ShipmentId: order.ShipmentId
        );
}
