using CustomCADs.Orders.Application.CompletedOrders.Commands.Internal.Create;
using CustomCADs.Orders.Application.CompletedOrders.Queries.Internal.ClientGetById;
using CustomCADs.Orders.Application.CompletedOrders.Queries.Internal.DesignerGetById;
using CustomCADs.Orders.Application.CompletedOrders.Queries.Internal.GetAll;
using CustomCADs.Shared.Core.Extensions;

namespace CustomCADs.Orders.Application.CompletedOrders;

internal static class Mapper
{
    internal static GetAllCompletedOrdersDto ToGetAllDto(this CompletedOrder order, string buyerUsername, string? designerUsername, string timeZone)
        => new(
            Id: order.Id,
            Name: order.Name,
            OrderedAt: order.OrderedAt.ToUserLocalTime(timeZone),
            PurchasedAt: order.PurchasedAt.ToUserLocalTime(timeZone),
            Delivery: order.Delivery,
            BuyerName: buyerUsername,
            DesignerName: designerUsername
        );

    internal static ClientGetCompletedOrderByIdDto ToClientGetByIdDto(this CompletedOrder order, string timeZone, string designer)
        => new(
            Id: order.Id,
            Name: order.Name,
            Description: order.Description,
            OrderedAt: order.OrderedAt.ToUserLocalTime(timeZone),
            PurchasedAt: order.PurchasedAt.ToUserLocalTime(timeZone),
            Delivery: order.Delivery,
            DesignerName: designer,
            ShipmentId: order.ShipmentId
        );

    internal static DesignerGetCompletedOrderByIdDto ToDesignerGetByIdDto(this CompletedOrder order, string buyer)
        => new(
            Id: order.Id,
            Name: order.Name,
            Description: order.Description,
            OrderedAt: order.OrderedAt,
            PurchasedAt: order.PurchasedAt,
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
            orderedAt: order.OrderedAt,
            buyerId: order.BuyerId,
            designerId: order.DesignerId,
            cadId: order.CadId
        );
}
