using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Customs.Domain.Customs.ValueObjects;

public record CustomSorting(
    CustomSortingType Type = CustomSortingType.OrderedAt,
    SortingDirection Direction = SortingDirection.Descending
);
