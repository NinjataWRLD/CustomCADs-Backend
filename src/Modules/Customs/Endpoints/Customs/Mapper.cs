using CustomCADs.Customs.Application.Customs.Queries.Internal.Client.GetById;
using CustomCADs.Customs.Application.Customs.Queries.Internal.Designer.GetById;
using CustomCADs.Customs.Application.Customs.Queries.Internal.Designer.GetCadUrlPost;
using CustomCADs.Customs.Application.Customs.Queries.Internal.Shared.GetAll;
using CustomCADs.Customs.Endpoints.Customs.Endpoints.Client.Get.CalculateShipment;
using CustomCADs.Customs.Endpoints.Customs.Endpoints.Client.Get.Recent;
using CustomCADs.Customs.Endpoints.Customs.Endpoints.Client.Get.Single;
using CustomCADs.Customs.Endpoints.Customs.Endpoints.Client.Post.Create;
using CustomCADs.Customs.Endpoints.Customs.Endpoints.Designer.Get.Single;
using CustomCADs.Customs.Endpoints.Customs.Endpoints.Designer.Patch.Finish;
using CustomCADs.Customs.Endpoints.Customs.Endpoints.Designer.Post;
using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Customs.Endpoints.Customs;

using static Constants;
using ClientGetCustomsRespose = Endpoints.Client.Get.All.GetCustomsResponse;
using DesignerGetCustomsRespose = Endpoints.Designer.Get.All.GetCustomsResponse;

internal static class Mapper
{
    internal static ClientGetCustomsRespose ToGetResponse(this GetAllCustomsDto custom)
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

    internal static PostCustomResponse ToPostResponse(this ClientGetCustomByIdDto custom)
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

    internal static GetCustomResponse ToResponse(this ClientGetCustomByIdDto custom)
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

    internal static GetCustomPostPresignedUrlResponse ToResponse(this GetCustomCadPresignedUrlPostDto dto)
        => new(
            CadKey: dto.GeneratedKey,
            CadUrl: dto.PresignedUrl
        );

    internal static (string Key, string ContentType, decimal Volume) ToTuple(this FinishCustomRequest req)
        => (Key: req.CadKey, ContentType: req.CadContentType, Volume: req.CadVolume);
}
