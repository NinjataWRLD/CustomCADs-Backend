using CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Create;
using CustomCADs.Customs.Application.Customs.Dtos;
using CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.GetById;
using CustomCADs.Customs.Application.Customs.Queries.Internal.Designer.GetById;
using CustomCADs.Customs.Application.Customs.Queries.Internal.Shared.GetAll;
using CustomCADs.Customs.Domain.Customs.Entities;

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

    internal static CustomerGetCustomByIdDto ToCustomerGetByIdDto(this Custom custom, AcceptedCustomDto? accepted, FinishedCustomDto? finished, CompletedCustomDto? completed)
        => new(
            Id: custom.Id,
            Name: custom.Name,
            Description: custom.Description,
            OrderedAt: custom.OrderedAt,
            ForDelivery: custom.ForDelivery,
            CustomStatus: custom.CustomStatus,
            AcceptedCustom: accepted,
            FinishedCustom: finished,
            CompletedCustom: completed
        );

    internal static DesignerGetCustomByIdDto ToDesignerGetByIdDto(this Custom custom, string buyer, AcceptedCustomDto? accepted, FinishedCustomDto? finished, CompletedCustomDto? completed)
        => new(
            Id: custom.Id,
            Name: custom.Name,
            Description: custom.Description,
            OrderedAt: custom.OrderedAt,
            ForDelivery: custom.ForDelivery,
            CustomStatus: custom.CustomStatus,
            BuyerName: buyer,
            AcceptedCustom: accepted,
            FinishedCustom: finished,
            CompletedCustom: completed
        );

    internal static Custom ToEntity(this CreateCustomCommand command)
        => Custom.Create(
            name: command.Name,
            description: command.Description,
            forDelivery: command.ForDelivery,
            buyerId: command.BuyerId
        );

    internal static AcceptedCustomDto ToDto(this AcceptedCustom custom, string designerName)
        => new(
            DesignerName: designerName,
            AcceptedAt: custom.AcceptedAt
        );

    internal static FinishedCustomDto ToDto(this FinishedCustom custom)
        => new(
            Price: custom.Price,
            FinishedAt: custom.FinishedAt,
            CadId: custom.CadId
        );

    internal static CompletedCustomDto ToDto(this CompletedCustom custom)
        => new(
            CustomizationId: custom.CustomizationId,
            ShipmentId: custom.ShipmentId
        );
}
