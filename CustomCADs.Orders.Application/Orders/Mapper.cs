using CustomCADs.Orders.Application.Orders.Commands.Create;
using CustomCADs.Orders.Application.Orders.Commands.CreateWithDelivery;
using CustomCADs.Orders.Application.Orders.Queries.CalculateShipment;
using CustomCADs.Orders.Application.Orders.Queries.DesignerGetById;
using CustomCADs.Orders.Application.Orders.Queries.GetAll;
using CustomCADs.Orders.Application.Orders.Queries.GetById;
using CustomCADs.Orders.Domain.Orders;
using CustomCADs.Shared.Application.Delivery.Dtos;

namespace CustomCADs.Orders.Application.Orders;

internal static class Mapper
{
    internal static GetAllOrdersDto ToGetAllOrdersItem(this Order order, string buyerUsername, string? designerUsername, string timeZone)
        => new(
            Id: order.Id,
            Name: order.Name,
            OrderDate: TimeZoneInfo.ConvertTimeFromUtc(
                order.OrderDate,
                TimeZoneInfo.FindSystemTimeZoneById(timeZone)
            ),
            Delivery: order.Delivery,
            OrderStatus: order.OrderStatus,
            BuyerName: buyerUsername,
            DesignerName: designerUsername
        );

    internal static GetOrderByIdDto ToGetOrderByIdDto(this Order order, string timeZone)
        => new(
            Id: order.Id,
            Name: order.Name,
            Description: order.Description,
            OrderDate: TimeZoneInfo.ConvertTimeFromUtc(
                order.OrderDate,
                TimeZoneInfo.FindSystemTimeZoneById(timeZone)
            ),
            Delivery: order.Delivery,
            OrderStatus: order.OrderStatus,
            DesignerId: order.DesignerId,
            CadId: order.CadId,
            ShipmentId: order.ShipmentId
        );
    
    internal static CalculateOrderShipmentDto ToCalculateOrderShipmentDto(this CalculationDto calculation, string timeZone)
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

    internal static DesignerGetOrderByIdDto ToDesignerGetOrderByIdDto(this Order order)
        => new(
            Id: order.Id,
            Name: order.Name,
            Description: order.Description,
            OrderDate: order.OrderDate,
            Delivery: order.Delivery,
            OrderStatus: order.OrderStatus,
            BuyerId: order.BuyerId,
            CadId: order.CadId,
            ShipmentId: order.ShipmentId
        );

    internal static Order ToOrder(this CreateOrderCommand command)
        => Order.Create(
            name: command.Name,
            description: command.Description,
            buyerId: command.BuyerId
        );

    internal static Order ToOrder(this CreateOrderWithDeliveryCommand command)
        => Order.CreateWithDelivery(
            name: command.Name,
            description: command.Description,
            buyerId: command.BuyerId
        );
}
