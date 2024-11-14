using CustomCADs.Cads.Domain.Cads.Enums;
using CustomCADs.Shared.Core.Domain.Enums;

namespace CustomCADs.Cads.Domain.Cads.ValueObjects;

public record CadSorting(
    CadSortingType Type = CadSortingType.CreationDate,
    SortingDirection Direction = SortingDirection.Descending
);
