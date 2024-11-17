using CustomCADs.Orders.Application.CustomOrders.Commands.Create;
using CustomCADs.Orders.Application.CustomOrders.Queries.GetAll;
using CustomCADs.Orders.Application.CustomOrders.Queries.GetById;
using CustomCADs.Orders.Domain.Common.Enums;
using CustomCADs.Orders.Domain.Common.Exceptions.CustomOrders;
using CustomCADs.Orders.Domain.CustomOrders.Entities;

namespace CustomCADs.Orders.Application.CustomOrders;

public static class Mapper
{
    public static GetAllCustomOrdersItem ToGetAllCustomOrdersItem(this CustomOrder order)
        => new(
            order.Id,
            order.Name,
            order.OrderDate,
            order.DeliveryType,
            order.OrderStatus,
            order.Image
        );

    public static GetCustomOrderByIdDto ToGetCustomOrderByIdDto(this CustomOrder order)
        => new(
            Id: order.Id,
            Name: order.Name,
            Description: order.Description,
            OrderDate: order.OrderDate,
            DeliveryType: order.DeliveryType,
            OrderStatus: order.OrderStatus,
            Image: order.Image,
            BuyerId: order.BuyerId,
            DesignerId: order.DesignerId,
            CadId: order.CadId,
            ShipmentId: order.ShipmentId
        );

    public static CustomOrder ToCustomerOrder(this CreateCustomOrderCommand req)
        => req.DeliveryType switch
        {
            DeliveryType.Physical => CustomOrder.CreatePhysical(
                req.Name,
                req.Description,
                req.BuyerId
            ),
            DeliveryType.Digital => CustomOrder.CreateDigital(
                req.Name,
                req.Description,
                req.BuyerId
            ),
            DeliveryType.Both => CustomOrder.CreateDigitalAndPhysical(
                req.Name,
                req.Description,
                req.BuyerId
            ),
            _ => throw CustomOrderValidationException.Custom("Invalid Delivery Type.")
        };
}
