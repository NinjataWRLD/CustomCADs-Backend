using CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Create;
using CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.GetById;
using CustomCADs.Customs.Application.Customs.Queries.Internal.Designer.GetById;
using CustomCADs.Customs.Application.Customs.Queries.Internal.Shared.GetAll;

namespace CustomCADs.Customs.Application.Customs;

internal static class Mapper
{
    internal static GetAllCustomsDto ToGetAllDto(this Custom custom, string buyerName, string? designerName)
        => new(
            Id: custom.Id,
            Name: custom.Name,
            ForDelivery: custom.ForDelivery,
            CustomStatus: custom.CustomStatus,
            OrderedAt: custom.OrderedAt,
            BuyerName: buyerName,
            DesignerName: designerName
        );
    internal static CustomerGetCustomByIdDto ToCustomerGetByIdDto(this Custom custom, string? designer)
        => new(
            Id: custom.Id,
            Name: custom.Name,
            Description: custom.Description,
            OrderedAt: custom.OrderedAt,
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
