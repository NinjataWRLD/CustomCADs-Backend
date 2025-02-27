using CustomCADs.Orders.Application.OngoingOrders.Commands.Create;
using CustomCADs.Orders.Application.OngoingOrders.Queries.CalculateShipment;
using CustomCADs.Orders.Application.OngoingOrders.Queries.ClientGetById;
using CustomCADs.Orders.Application.OngoingOrders.Queries.DesignerGetById;
using CustomCADs.Orders.Application.OngoingOrders.Queries.GetAll;
using CustomCADs.Shared.Abstractions.Delivery.Dtos;

namespace CustomCADs.Orders.Application.OngoingOrders;

internal static class Mapper
{
    internal static GetAllOngoingOrdersDto ToGetAllOrdersItem(this OngoingOrder order, string buyerUsername, string? designerUsername, string timeZone)
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

    internal static ClientGetOngoingOrderByIdDto ToGetOrderByIdDto(this OngoingOrder order, string timeZone, string? designer)
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
            DesignerName: designer
        );

    internal static CalculateOngoingOrderShipmentDto ToCalculateOrderShipmentDto(this CalculationDto calculation, string timeZone)
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

    internal static DesignerGetOngoingOrderByIdDto ToDesignerGetOrderByIdDto(this OngoingOrder order, string buyer)
        => new(
            Id: order.Id,
            Name: order.Name,
            Description: order.Description,
            OrderDate: order.OrderDate,
            Delivery: order.Delivery,
            OrderStatus: order.OrderStatus,
            BuyerName: buyer
        );

    internal static OngoingOrder ToOngoingOrder(this CreateOngoingOrderCommand command)
        => OngoingOrder.Create(
            name: command.Name,
            description: command.Description,
            delivery: command.Delivery,
            buyerId: command.BuyerId
        );
}
