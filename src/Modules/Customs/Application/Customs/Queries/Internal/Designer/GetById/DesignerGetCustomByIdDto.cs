﻿using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Customs;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Designer.GetById;

public record DesignerGetCustomByIdDto(
    CustomId Id,
    string Name,
    string Description,
    bool ForDelivery,
    string BuyerName,
    CustomStatus CustomStatus,
    DateTimeOffset OrderedAt
);
