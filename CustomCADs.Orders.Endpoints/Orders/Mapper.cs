using CustomCADs.Orders.Application.Orders.Queries.CalculateShipment;
using CustomCADs.Orders.Application.Orders.Queries.DesignerGetById;
using CustomCADs.Orders.Application.Orders.Queries.GetAll;
using CustomCADs.Orders.Application.Orders.Queries.GetById;
using CustomCADs.Orders.Endpoints.Orders.Client.Get.All;
using CustomCADs.Orders.Endpoints.Orders.Client.Get.CalculateShipment;
using CustomCADs.Orders.Endpoints.Orders.Client.Get.Recent;
using CustomCADs.Orders.Endpoints.Orders.Client.Get.Single;
using CustomCADs.Orders.Endpoints.Orders.Client.Post.Create;
using CustomCADs.Orders.Endpoints.Orders.Client.Post.CreateWithDelivery;
using CustomCADs.Orders.Endpoints.Orders.Designer.Get.Accepted;
using CustomCADs.Orders.Endpoints.Orders.Designer.Get.Begun;
using CustomCADs.Orders.Endpoints.Orders.Designer.Get.Completed;
using CustomCADs.Orders.Endpoints.Orders.Designer.Get.Finished;
using CustomCADs.Orders.Endpoints.Orders.Designer.Get.Pending;
using CustomCADs.Orders.Endpoints.Orders.Designer.Get.Reported;
using CustomCADs.Orders.Endpoints.Orders.Designer.Get.Single;
using CustomCADs.Orders.Endpoints.Orders.Designer.Patch.Finish;

namespace CustomCADs.Orders.Endpoints.Orders;

using static Constants;

internal static class Mapper
{
    internal static GetOrdersResponse ToGetOrdersDto(this GetAllOrdersDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            DeliveryType: order.Delivery.ToString(),
            OrderStatus: order.OrderStatus.ToString()
        );

    internal static RecentOrdersResponse ToRecentOrdersResponse(this GetAllOrdersDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            DesignerName: order.DesignerName
        );

    internal static PostOrderResponse ToPostOrderResponse(this GetOrderByIdDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            Description: order.Description,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            DeliveryType: order.Delivery.ToString(),
            OrderStatus: order.OrderStatus.ToString()
        );
    
    internal static PostOrderWithDeliveryResponse ToPostOrderWithDeliveryResponse(this GetOrderByIdDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            Description: order.Description,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            DeliveryType: order.Delivery.ToString(),
            OrderStatus: order.OrderStatus.ToString()
        );
    
    internal static CalculateOrderShipmentResponse ToCalculateOrderShipmentResponse(this CalculateOrderShipmentDto calculation)
        => new(
            Total: calculation.Total,
            Currency: calculation.Currency,
            PickupDate: calculation.PickupDate.ToString(SpeedyDateFormatString),
            DeliveryDeadline: calculation.DeliveryDeadline.ToString(SpeedyDateFormatString)
        );

    internal static GetOrderResponse ToGetOrderResponse(this GetOrderByIdDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            Description: order.Description,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            DeliveryType: order.Delivery.ToString(),
            OrderStatus: order.OrderStatus.ToString(),
            DesignerId: order.DesignerId?.Value,
            CadId: order.DesignerId?.Value,
            ShipmentId: order.DesignerId?.Value
        );

    internal static DesignerGetOrderResponse ToDesignerGetOrderResponse(this DesignerGetOrderByIdDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            Description: order.Description,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            DeliveryType: order.Delivery.ToString(),
            Status: order.OrderStatus.ToString(),
            BuyerId: order.BuyerId.Value,
            CadId: order.CadId?.Value,
            ShipmentId: order.ShipmentId?.Value
        );

    internal static GetPendingOrdersResponse ToGetPendingOrdersDto(this GetAllOrdersDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            DeliveryType: order.Delivery.ToString(),
            BuyerName: order.BuyerName
        );

    internal static GetAcceptedOrdersResponse ToGetAcceptedOrdersDto(this GetAllOrdersDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            DeliveryType: order.Delivery.ToString(),
            BuyerName: order.BuyerName
        );

    internal static GetBegunOrdersResponse ToGetBegunOrdersDto(this GetAllOrdersDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            DeliveryType: order.Delivery.ToString(),
            BuyerName: order.BuyerName
        );

    internal static GetFinishedOrdersResponse ToGetFinishedOrdersDto(this GetAllOrdersDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            DeliveryType: order.Delivery.ToString(),
            BuyerName: order.BuyerName
        );

    internal static GetCompletedOrdersResponse ToGetCompletedOrdersDto(this GetAllOrdersDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            DeliveryType: order.Delivery.ToString(),
            BuyerName: order.BuyerName
        );

    internal static GetReportedOrdersResponse ToGetReportedOrdersDto(this GetAllOrdersDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            DeliveryType: order.Delivery.ToString(),
            BuyerName: order.BuyerName
        );

    internal static (string Key, string ContentType) ToCadDto(this FinishOrderRequest req)
        => (Key: req.CadKey, ContentType: req.CadContentType);
}
