using CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.GetById;
using CustomCADs.Customs.Application.Customs.Queries.Internal.Designer.GetById;
using CustomCADs.Customs.Application.Customs.Queries.Internal.Designer.GetCadUrlPost;
using CustomCADs.Customs.Application.Customs.Queries.Internal.Shared.GetAll;
using CustomCADs.Customs.Endpoints.Customs.Endpoints.Customs.Get.All;
using CustomCADs.Customs.Endpoints.Customs.Endpoints.Customs.Get.CalculateShipment;
using CustomCADs.Customs.Endpoints.Customs.Endpoints.Customs.Get.Recent;
using CustomCADs.Customs.Endpoints.Customs.Endpoints.Customs.Get.Single;
using CustomCADs.Customs.Endpoints.Customs.Endpoints.Customs.Post.Create;
using CustomCADs.Customs.Endpoints.Customs.Endpoints.Designer.Get.Single;
using CustomCADs.Customs.Endpoints.Customs.Endpoints.Designer.Patch.Finish;
using CustomCADs.Customs.Endpoints.Customs.Endpoints.Designer.Post;
using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Customs.Endpoints.Customs;

using static Constants;
using CustomerGetCustomsRespose = GetCustomsResponse;
using DesignerGetCustomsRespose = Endpoints.Designer.Get.All.GetCustomsResponse;

internal static class Mapper
{
    internal static CustomerGetCustomsRespose ToGetResponse(this GetAllCustomsDto custom)
        => new(
            Id: custom.Id.Value,
            Name: custom.Name,
            OrderedAt: custom.OrderedAt.ToString(DateFormatString),
            ForDelivery: custom.ForDelivery,
            Status: custom.CustomStatus.ToString()
        );

    internal static RecentCustomsResponse ToRecentResponse(this GetAllCustomsDto custom)
        => new(
            Id: custom.Id.Value,
            Name: custom.Name,
            OrderedAt: custom.OrderedAt.ToString(DateFormatString),
            DesignerName: custom.DesignerName
        );

    internal static PostCustomResponse ToPostResponse(this CustomerGetCustomByIdDto custom)
        => new(
            Id: custom.Id.Value,
            Name: custom.Name,
            Description: custom.Description,
            OrderedAt: custom.OrderedAt.ToString(DateFormatString),
            ForDelivery: custom.ForDelivery,
            Status: custom.CustomStatus.ToString()
        );

    internal static CalculateCustomShipmentResponse ToResponse(this CalculateShipmentDto calculation)
        => new(
            Service: calculation.Service,
            Total: calculation.Total,
            Currency: calculation.Currency,
            PickupDate: calculation.PickupDate.ToString(SpeedyDateFormatString),
            DeliveryDeadline: calculation.DeliveryDeadline.ToString(SpeedyDateFormatString)
        );

    internal static GetCustomResponse ToResponse(this CustomerGetCustomByIdDto custom)
        => new(
            Id: custom.Id.Value,
            Name: custom.Name,
            Description: custom.Description,
            OrderedAt: custom.OrderedAt.ToString(DateFormatString),
            ForDelivery: custom.ForDelivery,
            Status: custom.CustomStatus.ToString(),
            DesignerName: custom.DesignerName
        );

    internal static DesignerGetCustomResponse ToResponse(this DesignerGetCustomByIdDto custom)
        => new(
            Id: custom.Id.Value,
            Name: custom.Name,
            Description: custom.Description,
            OrderedAt: custom.OrderedAt.ToString(DateFormatString),
            ForDelivery: custom.ForDelivery,
            Status: custom.CustomStatus.ToString(),
            BuyerName: custom.BuyerName
        );

    internal static DesignerGetCustomsRespose ToResponse(this GetAllCustomsDto custom)
        => new(
            Id: custom.Id.Value,
            Name: custom.Name,
            OrderedAt: custom.OrderedAt.ToString(DateFormatString),
            ForDelivery: custom.ForDelivery,
            BuyerName: custom.BuyerName
        );

    internal static (string Key, string ContentType, decimal Volume) ToTuple(this FinishCustomRequest req)
        => (Key: req.CadKey, ContentType: req.CadContentType, Volume: req.CadVolume);
}
