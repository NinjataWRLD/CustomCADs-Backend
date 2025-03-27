using CustomCADs.Orders.Application.CompletedOrders.Queries.Internal.ClientGetById;
using CustomCADs.Orders.Application.CompletedOrders.Queries.Internal.DesignerGetById;
using CustomCADs.Orders.Application.CompletedOrders.Queries.Internal.GetAll;
using CustomCADs.Orders.Endpoints.CompletedOrders.Endpoints.Client.Get.All;
using CustomCADs.Orders.Endpoints.CompletedOrders.Endpoints.Client.Get.Single;
using CustomCADs.Orders.Endpoints.CompletedOrders.Endpoints.Designer.Get.All;
using CustomCADs.Orders.Endpoints.CompletedOrders.Endpoints.Designer.Get.Single;

namespace CustomCADs.Orders.Endpoints.CompletedOrders;

using static Constants;

internal static class Mapper
{
    internal static GetCompletedOrdersResponse ToClientResponse(this GetAllCompletedOrdersDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderedAt: order.OrderedAt.ToString(DateFormatString),
            PurchasedAt: order.PurchasedAt.ToString(DateFormatString),
            Delivery: order.Delivery
        );

    internal static GetCompletedOrderResponse ToResponse(this ClientGetCompletedOrderByIdDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            Description: order.Description,
            OrderedAt: order.OrderedAt.ToString(DateFormatString),
            PurchasedAt: order.PurchasedAt.ToString(DateFormatString),
            Delivery: order.Delivery,
            DesignerName: order.DesignerName,
            ShipmentId: order.ShipmentId?.Value
        );

    internal static DesignerGetCompletedOrdersResponse ToDesignerResponse(this GetAllCompletedOrdersDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            BuyerName: order.BuyerName,
            OrderedAt: order.OrderedAt.ToString(DateFormatString),
            PurchasedAt: order.PurchasedAt.ToString(DateFormatString),
            Delivery: order.Delivery
        );

    internal static DesignerGetCompletedOrderResponse ToResponse(this DesignerGetCompletedOrderByIdDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            Description: order.Description,
            OrderedAt: order.OrderedAt.ToString(DateFormatString),
            PurchasedAt: order.PurchasedAt.ToString(DateFormatString),
            Delivery: order.Delivery,
            BuyerName: order.BuyerName,
            ShipmentId: order.ShipmentId?.Value
        );
}
