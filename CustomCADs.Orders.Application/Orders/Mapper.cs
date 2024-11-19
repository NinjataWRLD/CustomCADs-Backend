﻿using CustomCADs.Orders.Application.Orders.Commands.Create;
using CustomCADs.Orders.Application.Orders.Queries.GetAll;
using CustomCADs.Orders.Application.Orders.Queries.GetById;
using CustomCADs.Orders.Domain.Common.Exceptions.Orders;
using CustomCADs.Orders.Domain.Orders.Entities;
using CustomCADs.Orders.Domain.Orders.Enums;

namespace CustomCADs.Orders.Application.Orders;

public static class Mapper
{
    public static GetAllOrdersItem ToGetAllOrdersItem(this Order order, string designerName)
        => new(
            order.Id,
            order.Name,
            order.OrderDate,
            order.DeliveryType,
            order.OrderStatus,
            order.Image,
            designerName
        );

    public static GetOrderByIdDto ToGetOrderByIdDto(this Order order)
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

    public static Order ToOrder(this CreateOrderCommand command)
        => command.DeliveryType switch
        {
            DeliveryType.Physical => Order.CreatePhysical(
                command.Name,
                command.Description,
                command.BuyerId
            ),
            DeliveryType.Digital => Order.CreateDigital(
                command.Name,
                command.Description,
                command.BuyerId
            ),
            DeliveryType.Both => Order.CreateDigitalAndPhysical(
                command.Name,
                command.Description,
                command.BuyerId
            ),
            _ => throw OrderValidationException.Custom("Invalid Delivery Type.")
        };
}