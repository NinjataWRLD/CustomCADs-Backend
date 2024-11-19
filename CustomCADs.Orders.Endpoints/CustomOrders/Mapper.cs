using CustomCADs.Orders.Application.CustomOrders.Queries.GetAll;
using CustomCADs.Orders.Application.CustomOrders.Queries.GetById;
using CustomCADs.Orders.Endpoints.CustomOrders.Get;
using CustomCADs.Orders.Endpoints.CustomOrders.GetAll;
using CustomCADs.Orders.Endpoints.CustomOrders.Post;
using CustomCADs.Orders.Endpoints.CustomOrders.Recent;

namespace CustomCADs.Orders.Endpoints.CustomOrders;

using static Constants;

public static class Mapper
{
    public static GetCustomOrdersDto ToGetCustomOrdersDto(this GetAllCustomOrdersItem item)
        => new(
            Id: item.Id.Value,
            Name: item.Name,
            OrderDate: item.OrderDate.ToString(DateFormatString),
            DeliveryType: item.DeliveryType.ToString(),
            OrderStatus: item.OrderStatus.ToString(),
            Image: new(item.Image)
        );

    public static RecentCustomOrdersResponse ToRecentCustomOrdersResponse(this GetAllCustomOrdersItem item)
        => new(
            Id: item.Id.Value,
            Name: item.Name,
            OrderDate: item.OrderDate.ToString(DateFormatString),
            DesignerName: item.DesignerName
        );

    public static PostCustomOrderResponse ToPostCustomOrderResponse(this GetCustomOrderByIdDto item)
        => new(
            Id: item.Id.Value,
            Name: item.Name,
            Description: item.Description,
            OrderDate: item.OrderDate.ToString(DateFormatString),
            DeliveryType: item.DeliveryType.ToString(),
            OrderStatus: item.OrderStatus.ToString(),
            BuyerId: item.BuyerId.Value
        );

    public static GetCustomOrderResponse ToGetCustomOrderResponse(this GetCustomOrderByIdDto item)
        => new(
            Id: item.Id.Value,
            Name: item.Name,
            Description: item.Description,
            OrderDate: item.OrderDate.ToString(DateFormatString),
            DeliveryType: item.DeliveryType.ToString(),
            OrderStatus: item.OrderStatus.ToString(),
            Image: new(item.Image),
            BuyerId: item.BuyerId.Value,
            DesignerId: item.DesignerId?.Value,
            CadId: item.DesignerId?.Value,
            ShipmentId: item.DesignerId?.Value
        );
}
