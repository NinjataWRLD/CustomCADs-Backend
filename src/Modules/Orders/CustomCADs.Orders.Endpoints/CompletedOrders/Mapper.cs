using CustomCADs.Orders.Application.CompletedOrders.Queries.ClientGetById;
using CustomCADs.Orders.Application.CompletedOrders.Queries.DesignerGetById;
using CustomCADs.Orders.Application.CompletedOrders.Queries.GetAll;
using CustomCADs.Orders.Endpoints.CompletedOrders.Client.Get.All;
using CustomCADs.Orders.Endpoints.CompletedOrders.Client.Get.Single;
using CustomCADs.Orders.Endpoints.CompletedOrders.Designer.Get.All;
using CustomCADs.Orders.Endpoints.CompletedOrders.Designer.Get.Single;

namespace CustomCADs.Orders.Endpoints.CompletedOrders;

using static Constants;

internal static class Mapper
{
    internal static GetCompletedOrdersResponse ToGetCompletedOrdersResponse(this GetAllCompletedOrdersDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            PurchaseDate: order.PurchaseDate.ToString(DateFormatString),
            Delivery: order.Delivery
        );

    internal static GetCompletedOrderResponse ToGetCompletedOrderResponse(this ClientGetCompletedOrderByIdDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            Description: order.Description,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            PurchaseDate: order.PurchaseDate.ToString(DateFormatString),
            Delivery: order.Delivery,
            DesignerId: order.DesignerId.Value,
            CadId: order.CadId.Value,
            ShipmentId: order.ShipmentId?.Value
        );

    internal static DesignerGetCompletedOrderResponse ToDesignerGetOrderResponse(this DesignerGetCompletedOrderByIdDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            Description: order.Description,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            PurchaseDate: order.PurchaseDate.ToString(DateFormatString),
            Delivery: order.Delivery,
            BuyerId: order.BuyerId.Value,
            CadId: order.CadId.Value,
            ShipmentId: order.ShipmentId?.Value
        );

    internal static DesignerGetCompletedOrdersResponse ToDesignerGetCompletedOrdersResponse(this GetAllCompletedOrdersDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            BuyerName: order.BuyerName,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            PurchaseDate: order.PurchaseDate.ToString(DateFormatString),
            Delivery: order.Delivery
        );
}
