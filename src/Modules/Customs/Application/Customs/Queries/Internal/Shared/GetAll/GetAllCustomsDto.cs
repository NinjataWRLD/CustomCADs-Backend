using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Customs;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Shared.GetAll;

public record GetAllCustomsDto(
    CustomId Id,
    string Name,
    bool ForDelivery,
    CustomStatus CustomStatus,
    DateTimeOffset OrderedAt,
    string BuyerName,
    string? DesignerName
);