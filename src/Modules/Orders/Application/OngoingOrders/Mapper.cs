﻿using CustomCADs.Orders.Application.OngoingOrders.Commands.Internal.Create;
using CustomCADs.Orders.Application.OngoingOrders.Queries.Internal.ClientGetById;
using CustomCADs.Orders.Application.OngoingOrders.Queries.Internal.DesignerGetById;
using CustomCADs.Orders.Application.OngoingOrders.Queries.Internal.GetAll;
using CustomCADs.Shared.Core.Extensions;

namespace CustomCADs.Orders.Application.OngoingOrders;

internal static class Mapper
{
    internal static GetAllOngoingOrdersDto ToGetAllDto(this OngoingOrder order, string buyerUsername, string? designerUsername, string timeZone)
        => new(
            Id: order.Id,
            Name: order.Name,
            OrderedAt: order.OrderedAt.ToUserLocalTime(timeZone),
            Delivery: order.Delivery,
            OrderStatus: order.OrderStatus,
            BuyerName: buyerUsername,
            DesignerName: designerUsername
        );

    internal static ClientGetOngoingOrderByIdDto ToClientGetByIdDto(this OngoingOrder order, string timeZone, string? designer)
        => new(
            Id: order.Id,
            Name: order.Name,
            Description: order.Description,
            OrderedAt: order.OrderedAt.ToUserLocalTime(timeZone),
            Delivery: order.Delivery,
            OrderStatus: order.OrderStatus,
            DesignerName: designer
        );

    internal static DesignerGetOngoingOrderByIdDto ToDesignerGetByIdDto(this OngoingOrder order, string buyer)
        => new(
            Id: order.Id,
            Name: order.Name,
            Description: order.Description,
            OrderedAt: order.OrderedAt,
            Delivery: order.Delivery,
            OrderStatus: order.OrderStatus,
            BuyerName: buyer
        );

    internal static OngoingOrder ToEntity(this CreateOngoingOrderCommand command)
        => OngoingOrder.Create(
            name: command.Name,
            description: command.Description,
            delivery: command.Delivery,
            buyerId: command.BuyerId
        );
}
