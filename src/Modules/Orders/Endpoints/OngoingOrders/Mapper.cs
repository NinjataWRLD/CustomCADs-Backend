using CustomCADs.Orders.Application.OngoingOrders.Queries.Internal.ClientGetById;
using CustomCADs.Orders.Application.OngoingOrders.Queries.Internal.DesignerGetById;
using CustomCADs.Orders.Application.OngoingOrders.Queries.Internal.GetAll;
using CustomCADs.Orders.Application.OngoingOrders.Queries.Internal.GetCadUrlPost;
using CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Client.Get.All;
using CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Client.Get.CalculateShipment;
using CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Client.Get.Recent;
using CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Client.Get.Single;
using CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Client.Post.Create;
using CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Designer.Get.Accepted;
using CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Designer.Get.Begun;
using CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Designer.Get.Finished;
using CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Designer.Get.Pending;
using CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Designer.Get.Reported;
using CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Designer.Get.Single;
using CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Designer.Patch.Finish;
using CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Designer.Post;
using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Orders.Endpoints.OngoingOrders;

using static Constants;

internal static class Mapper
{
    internal static GetOngoingOrdersResponse ToGetResponse(this GetAllOngoingOrdersDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            Delivery: order.Delivery,
            OrderStatus: order.OrderStatus.ToString()
        );

    internal static RecentOngoingOrdersResponse ToRecentResponse(this GetAllOngoingOrdersDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            DesignerName: order.DesignerName
        );

    internal static PostOngoingOrderResponse ToPostResponse(this ClientGetOngoingOrderByIdDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            Description: order.Description,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            Delivery: order.Delivery,
            OrderStatus: order.OrderStatus.ToString()
        );

    internal static CalculateOngoingOrderShipmentResponse ToResponse(this CalculateShipmentDto calculation)
        => new(
            Service: calculation.Service,
            Total: calculation.Total,
            Currency: calculation.Currency,
            PickupDate: calculation.PickupDate.ToString(SpeedyDateFormatString),
            DeliveryDeadline: calculation.DeliveryDeadline.ToString(SpeedyDateFormatString)
        );

    internal static GetOngoingOrderResponse ToResponse(this ClientGetOngoingOrderByIdDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            Description: order.Description,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            Delivery: order.Delivery,
            OrderStatus: order.OrderStatus.ToString(),
            DesignerName: order.DesignerName
        );

    internal static DesignerGetOngoingOrderResponse ToResponse(this DesignerGetOngoingOrderByIdDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            Description: order.Description,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            Delivery: order.Delivery,
            Status: order.OrderStatus.ToString(),
            BuyerName: order.BuyerName
        );

    internal static GetPendingOngoingOrdersResponse ToPendingResponse(this GetAllOngoingOrdersDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            Delivery: order.Delivery,
            BuyerName: order.BuyerName
        );

    internal static GetAcceptedOngoingOrdersResponse ToAcceptedResponse(this GetAllOngoingOrdersDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            Delivery: order.Delivery,
            BuyerName: order.BuyerName
        );

    internal static GetBegunOngoingOrdersResponse ToBegunResponse(this GetAllOngoingOrdersDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            Delivery: order.Delivery,
            BuyerName: order.BuyerName
        );

    internal static GetFinishedOngoingOrdersResponse ToFinishedResponse(this GetAllOngoingOrdersDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            Delivery: order.Delivery,
            BuyerName: order.BuyerName
        );

    internal static GetReportedOngoingOrdersResponse ToReportedResponse(this GetAllOngoingOrdersDto order)
        => new(
            Id: order.Id.Value,
            Name: order.Name,
            OrderDate: order.OrderDate.ToString(DateFormatString),
            Delivery: order.Delivery,
            BuyerName: order.BuyerName
        );

    internal static GetOngoingOrderPostPresignedUrlResponse ToResponse(this GetOngoingOrderCadPresignedUrlPostDto dto)
        => new(
            CadKey: dto.GeneratedKey,
            CadUrl: dto.PresignedUrl
        );

    internal static (string Key, string ContentType, decimal Volume) ToTuple(this FinishOngoingOrderRequest req)
        => (Key: req.CadKey, ContentType: req.CadContentType, Volume: req.CadVolume);
}
