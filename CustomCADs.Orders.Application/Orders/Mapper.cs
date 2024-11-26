using CustomCADs.Orders.Application.Orders.Commands.Create;
using CustomCADs.Orders.Application.Orders.Queries.DesignerGetById;
using CustomCADs.Orders.Application.Orders.Queries.GetAll;
using CustomCADs.Orders.Application.Orders.Queries.GetById;
using CustomCADs.Orders.Domain.Common.Exceptions.Orders;
using CustomCADs.Orders.Domain.Orders;
using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;

namespace CustomCADs.Orders.Application.Orders;

public static class Mapper
{
    public static GetAllOrdersDto ToGetAllOrdersItem(this Order order, string buyerUsername, string? designerUsername)
        => new(
            Id: order.Id,
            Name: order.Name,
            OrderDate: order.OrderDate,
            DeliveryType: order.DeliveryType,
            OrderStatus: order.OrderStatus,
            BuyerName: buyerUsername,
            DesignerName: designerUsername
        );

    public static GetOrderByIdDto ToGetOrderByIdDto(this Order order)
        => new(
            Id: order.Id,
            Name: order.Name,
            Description: order.Description,
            OrderDate: order.OrderDate,
            DeliveryType: order.DeliveryType,
            OrderStatus: order.OrderStatus,
            DesignerId: order.DesignerId,
            CadId: order.CadId,
            ShipmentId: order.ShipmentId
        );

    public static DesignerGetOrderByIdDto ToDesignerGetOrderByIdDto(this Order order)
        => new(
            Id: order.Id,
            Name: order.Name,
            Description: order.Description,
            OrderDate: order.OrderDate,
            DeliveryType: order.DeliveryType,
            OrderStatus: order.OrderStatus,
            BuyerId: order.BuyerId,
            CadId: order.CadId,
            ShipmentId: order.ShipmentId
        );

    public static Order ToOrder(this CreateOrderCommand command, ShipmentId? shipmentId)
        => command.DeliveryType switch
        {
            DeliveryType.Physical => Order.CreatePhysical(
                name: command.Name,
                description: command.Description,
                buyerId: command.BuyerId,
                shipmentId: shipmentId
            ),
            DeliveryType.Digital => Order.CreateDigital(
                name: command.Name,
                description: command.Description,
                buyerId: command.BuyerId
            ),
            DeliveryType.Both => Order.CreateDigitalAndPhysical(
                name: command.Name,
                description: command.Description,
                buyerId: command.BuyerId,
                shipmentId: shipmentId
            ),
            _ => throw OrderValidationException.Custom("Invalid Delivery Type.")
        };
}
