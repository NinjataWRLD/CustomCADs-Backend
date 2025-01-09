﻿using CustomCADs.Orders.Application.OngoingOrders.Commands.Status.Finish;
using CustomCADs.Orders.Application.OngoingOrders.Queries.CalculateShipment;
using CustomCADs.Orders.Application.OngoingOrders.Queries.ClientGetById;
using CustomCADs.Orders.Application.OngoingOrders.Queries.DesignerGetById;
using CustomCADs.Orders.Application.OngoingOrders.Queries.GetAll;
using CustomCADs.Orders.Endpoints.OngoingOrders.Client.Get.All;
using CustomCADs.Orders.Endpoints.OngoingOrders.Client.Get.CalculateShipment;
using CustomCADs.Orders.Endpoints.OngoingOrders.Client.Get.Recent;
using CustomCADs.Orders.Endpoints.OngoingOrders.Client.Get.Single;
using CustomCADs.Orders.Endpoints.OngoingOrders.Client.Post.Create;
using CustomCADs.Orders.Endpoints.OngoingOrders.Designer.Get.Accepted;
using CustomCADs.Orders.Endpoints.OngoingOrders.Designer.Get.Begun;
using CustomCADs.Orders.Endpoints.OngoingOrders.Designer.Get.Finished;
using CustomCADs.Orders.Endpoints.OngoingOrders.Designer.Get.Pending;
using CustomCADs.Orders.Endpoints.OngoingOrders.Designer.Get.Reported;
using CustomCADs.Orders.Endpoints.OngoingOrders.Designer.Get.Single;
using CustomCADs.Orders.Endpoints.OngoingOrders.Designer.Patch.Finish;

namespace CustomCADs.Orders.Endpoints.OngoingOrders;

using static Constants;

internal static class Mapper
{
    internal static GetOngoingOrdersResponse ToGetOrdersDto(this GetAllOngoingOrdersDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            Delivery: order.Delivery,
            OrderStatus: order.OrderStatus.ToString()
        );

    internal static RecentOngoingOrdersResponse ToRecentOrdersResponse(this GetAllOngoingOrdersDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            DesignerName: order.DesignerName
        );

    internal static PostOngoingOrderResponse ToPostOrderResponse(this ClientGetOngoingOrderByIdDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            Description: order.Description,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            Delivery: order.Delivery,
            OrderStatus: order.OrderStatus.ToString()
        );

    internal static CalculateOngoingOrderShipmentResponse ToCalculateOrderShipmentResponse(this CalculateOngoingOrderShipmentDto calculation)
        => new(
            Service: calculation.Service,
            Total: calculation.Total,
            Currency: calculation.Currency,
            PickupDate: calculation.PickupDate.ToString(SpeedyDateFormatString),
            DeliveryDeadline: calculation.DeliveryDeadline.ToString(SpeedyDateFormatString)
        );

    internal static GetOngoingOrderResponse ToGetOrderResponse(this ClientGetOngoingOrderByIdDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            Description: order.Description,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            Delivery: order.Delivery,
            OrderStatus: order.OrderStatus.ToString(),
            DesignerId: order.DesignerId?.Value
        );

    internal static DesignerGetOngoingOrderResponse ToDesignerGetOrderResponse(this DesignerGetOngoingOrderByIdDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            Description: order.Description,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            Delivery: order.Delivery,
            Status: order.OrderStatus.ToString(),
            BuyerId: order.BuyerId.Value
        );

    internal static GetPendingOngoingOrdersResponse ToGetPendingOrdersDto(this GetAllOngoingOrdersDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            Delivery: order.Delivery,
            BuyerName: order.BuyerName
        );

    internal static GetAcceptedOngoingOrdersResponse ToGetAcceptedOrdersDto(this GetAllOngoingOrdersDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            Delivery: order.Delivery,
            BuyerName: order.BuyerName
        );

    internal static GetBegunOngoingOrdersResponse ToGetBegunOrdersDto(this GetAllOngoingOrdersDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            Delivery: order.Delivery,
            BuyerName: order.BuyerName
        );

    internal static GetFinishedOngoingOrdersResponse ToGetFinishedOrdersDto(this GetAllOngoingOrdersDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            Delivery: order.Delivery,
            BuyerName: order.BuyerName
        );

    internal static FinishOngoingOrderResponse ToFinishOrderResponse(this FinishOngoingOrderDto dto)
        => new(
            PresignedKey: dto.PresignedKey,
            GeneratedUrl: dto.GeneratedUrl
        );

    internal static GetReportedOngoingOrdersResponse ToGetReportedOrdersDto(this GetAllOngoingOrdersDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            Delivery: order.Delivery,
            BuyerName: order.BuyerName
        );

    internal static (string Key, string ContentType) ToCadDto(this FinishOngoingOrderRequest req)
        => (Key: req.CadKey, ContentType: req.CadContentType);
}
