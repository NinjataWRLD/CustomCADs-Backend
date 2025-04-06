using CustomCADs.Customs.Domain.Customs.Enums;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.GetById;

public record CustomerGetCustomByIdDto(
    CustomId Id,
    string Name,
    string Description,
    bool ForDelivery,
    string? DesignerName,
    CustomStatus CustomStatus,
    DateTimeOffset OrderedAt
);
