using CustomCADs.Orders.Application.Orders.Queries.GetAll;
using CustomCADs.Orders.Application.Orders.Queries.GetById;
using CustomCADs.Orders.Endpoints.Client.Get.All;
using CustomCADs.Orders.Endpoints.Client.Get.Recent;
using CustomCADs.Orders.Endpoints.Client.Get.Single;
using CustomCADs.Orders.Endpoints.Client.Post;
using CustomCADs.Orders.Endpoints.Designer.Get.Accepted;
using CustomCADs.Orders.Endpoints.Designer.Get.Begun;
using CustomCADs.Orders.Endpoints.Designer.Get.Finished;
using CustomCADs.Orders.Endpoints.Designer.Get.Pending;
using CustomCADs.Orders.Endpoints.Designer.Get.Reported;
using CustomCADs.Orders.Endpoints.Designer.Get.Single;

namespace CustomCADs.Orders.Endpoints.Client;

using static Constants;

public static class Mapper
{
    public static GetOrdersDto ToGetOrdersDto(this GetAllOrdersItem order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            DeliveryType: order.DeliveryType.ToString(),
            OrderStatus: order.OrderStatus.ToString(),
            Image: new(order.Image.Key, order.Image.ContentType)
        );

    public static RecentOrdersResponse ToRecentOrdersResponse(this GetAllOrdersItem order)
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
            OrderStatus: order.OrderStatus.ToString(),
            BuyerId: order.BuyerId.Value
        );

    public static GetOrderResponse ToGetOrderResponse(this GetOrderByIdDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            Description: order.Description,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            DeliveryType: order.DeliveryType.ToString(),
            OrderStatus: order.OrderStatus.ToString(),
            Image: new(order.Image.Key, order.Image.ContentType),
            BuyerId: order.BuyerId.Value,
            DesignerId: order.DesignerId?.Value,
            CadId: order.DesignerId?.Value,
            ShipmentId: order.DesignerId?.Value
        );

    public static DesignerGetOrderResponse ToDesignerGetOrderResponse(this GetOrderByIdDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            Description: order.Description,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            DeliveryType: order.DeliveryType.ToString(),
            Status: order.OrderStatus.ToString(),
            Image: new(order.Image.Key, order.Image.ContentType),
            BuyerId: order.BuyerId.Value,
            CadId: order.DesignerId?.Value,
            ShipmentId: order.DesignerId?.Value
        );

    public static GetPendingOrdersDto ToGetPendingOrdersDto(this GetAllOrdersItem order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            DeliveryType: order.DeliveryType.ToString(),
            BuyerName: order.BuyerName
        );
    
    public static GetAcceptedOrdersDto ToGetAcceptedOrdersDto(this GetAllOrdersItem order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            DeliveryType: order.DeliveryType.ToString(),
            BuyerName: order.BuyerName
        );
    
    public static GetBegunOrdersDto ToGetBegunOrdersDto(this GetAllOrdersItem order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            DeliveryType: order.DeliveryType.ToString(),
            BuyerName: order.BuyerName
        );

    public static GetFinishedOrdersDto ToGetFinishedOrdersDto(this GetAllOrdersItem order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            DeliveryType: order.DeliveryType.ToString(),
            BuyerName: order.BuyerName
        );

    public static GetReportedOrdersDto ToGetReportedOrdersDto(this GetAllOrdersItem order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            DeliveryType: order.DeliveryType.ToString(),
            BuyerName: order.BuyerName
        );
}
