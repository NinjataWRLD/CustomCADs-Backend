using CustomCADs.Orders.Application.Orders.Queries.GetAll;
using CustomCADs.Orders.Application.Orders.Queries.GetById;
using CustomCADs.Orders.Endpoints.Orders.Get.All;
using CustomCADs.Orders.Endpoints.Orders.Get.Recent;
using CustomCADs.Orders.Endpoints.Orders.Get.Single;
using CustomCADs.Orders.Endpoints.Orders.Post;

namespace CustomCADs.Orders.Endpoints.Orders;

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
            Image: new(order.Image)
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
            Image: new(order.Image),
            BuyerId: order.BuyerId.Value,
            DesignerId: order.DesignerId?.Value,
            CadId: order.DesignerId?.Value,
            ShipmentId: order.DesignerId?.Value
        );
}
