using CustomCADs.Orders.Application.Orders.Queries.DesignerGetById;
using CustomCADs.Orders.Application.Orders.Queries.GetAll;
using CustomCADs.Orders.Application.Orders.Queries.GetById;
using CustomCADs.Orders.Endpoints.Client.Get.All;
using CustomCADs.Orders.Endpoints.Client.Get.Recent;
using CustomCADs.Orders.Endpoints.Client.Get.Single;
using CustomCADs.Orders.Endpoints.Client.Post.Orders;
using CustomCADs.Orders.Endpoints.Designer.Get.Accepted;
using CustomCADs.Orders.Endpoints.Designer.Get.Begun;
using CustomCADs.Orders.Endpoints.Designer.Get.Completed;
using CustomCADs.Orders.Endpoints.Designer.Get.Finished;
using CustomCADs.Orders.Endpoints.Designer.Get.Pending;
using CustomCADs.Orders.Endpoints.Designer.Get.Reported;
using CustomCADs.Orders.Endpoints.Designer.Get.Single;

namespace CustomCADs.Orders.Endpoints.Client;

using static Constants;

public static class Mapper
{
    public static GetOrdersDto ToGetOrdersDto(this GetAllOrdersDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            DeliveryType: order.DeliveryType.ToString(),
            OrderStatus: order.OrderStatus.ToString()
        );

    public static RecentOrdersResponse ToRecentOrdersResponse(this GetAllOrdersDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            DesignerName: order.DesignerName
        );

    public static PostOrderResponse ToPostOrderResponse(this GetOrderByIdDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            Description: order.Description,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            DeliveryType: order.DeliveryType.ToString(),
            OrderStatus: order.OrderStatus.ToString()
        );

    public static GetOrderResponse ToGetOrderResponse(this GetOrderByIdDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            Description: order.Description,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            DeliveryType: order.DeliveryType.ToString(),
            OrderStatus: order.OrderStatus.ToString(),
            DesignerId: order.DesignerId?.Value,
            CadId: order.DesignerId?.Value,
            ShipmentId: order.DesignerId?.Value
        );

    public static DesignerGetOrderResponse ToDesignerGetOrderResponse(this DesignerGetOrderByIdDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            Description: order.Description,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            DeliveryType: order.DeliveryType.ToString(),
            Status: order.OrderStatus.ToString(),
            BuyerId: order.BuyerId.Value,
            CadId: order.CadId?.Value,
            ShipmentId: order.ShipmentId?.Value
        );

    public static GetPendingOrdersDto ToGetPendingOrdersDto(this GetAllOrdersDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            DeliveryType: order.DeliveryType.ToString(),
            BuyerName: order.BuyerName
        );

    public static GetAcceptedOrdersDto ToGetAcceptedOrdersDto(this GetAllOrdersDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            DeliveryType: order.DeliveryType.ToString(),
            BuyerName: order.BuyerName
        );

    public static GetBegunOrdersDto ToGetBegunOrdersDto(this GetAllOrdersDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            DeliveryType: order.DeliveryType.ToString(),
            BuyerName: order.BuyerName
        );

    public static GetFinishedOrdersDto ToGetFinishedOrdersDto(this GetAllOrdersDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            DeliveryType: order.DeliveryType.ToString(),
            BuyerName: order.BuyerName
        );
    
    public static GetCompletedOrdersDto ToGetCompletedOrdersDto(this GetAllOrdersDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            DeliveryType: order.DeliveryType.ToString(),
            BuyerName: order.BuyerName
        );

    public static GetReportedOrdersDto ToGetReportedOrdersDto(this GetAllOrdersDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            DeliveryType: order.DeliveryType.ToString(),
            BuyerName: order.BuyerName
        );
}
