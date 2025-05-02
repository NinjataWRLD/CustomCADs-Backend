using CustomCADs.Customs.Application.Customs.Dtos;
using CustomCADs.Customs.Domain.Customs.Enums;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Designer.GetById;

public record DesignerGetCustomByIdDto(
    CustomId Id,
    string Name,
    string Description,
    bool ForDelivery,
    string BuyerName,
    CustomStatus CustomStatus,
    DateTimeOffset OrderedAt,
    AcceptedCustomDto? AcceptedCustom,
    FinishedCustomDto? FinishedCustom,
    CompletedCustomDto? CompletedCustom
);
