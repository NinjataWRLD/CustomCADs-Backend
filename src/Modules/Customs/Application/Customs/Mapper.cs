using CustomCADs.Customs.Application.Customs.Commands.Internal.Client.Create;
using CustomCADs.Customs.Application.Customs.Queries.Internal.Client.GetById;
using CustomCADs.Customs.Application.Customs.Queries.Internal.Designer.GetById;
using CustomCADs.Customs.Application.Customs.Queries.Internal.Shared.GetAll;
using CustomCADs.Shared.Core.Extensions;

namespace CustomCADs.Customs.Application.Customs;

internal static class Mapper
{
    internal static GetAllCustomsDto ToGetAllDto(this Custom custom, string buyerName, string? designerName, string timeZone)
        => new(
            Id: custom.Id,
            Name: custom.Name,
            ForDelivery: custom.ForDelivery,
            CustomStatus: custom.CustomStatus,
            OrderedAt: custom.OrderedAt.ToUserLocalTime(timeZone),
            BuyerName: buyerName,
            DesignerName: designerName
        );
    internal static ClientGetCustomByIdDto ToClientGetByIdDto(this Custom custom, string timeZone, string? designer)
        => new(
            Id: custom.Id,
            Name: custom.Name,
            Description: custom.Description,
            OrderedAt: custom.OrderedAt.ToUserLocalTime(timeZone),
            ForDelivery: custom.ForDelivery,
            CustomStatus: custom.CustomStatus,
            DesignerName: designer
        );

    internal static DesignerGetCustomByIdDto ToDesignerGetByIdDto(this Custom custom, string buyer)
        => new(
            Id: custom.Id,
            Name: custom.Name,
            Description: custom.Description,
            OrderedAt: custom.OrderedAt,
            ForDelivery: custom.ForDelivery,
            CustomStatus: custom.CustomStatus,
            BuyerName: buyer
        );

    internal static Custom ToEntity(this CreateCustomCommand command)
        => Custom.Create(
            name: command.Name,
            description: command.Description,
            forDelivery: command.ForDelivery,
            buyerId: command.BuyerId
        );
}
