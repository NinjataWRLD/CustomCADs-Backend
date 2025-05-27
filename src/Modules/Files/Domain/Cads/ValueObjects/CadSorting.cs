using CustomCADs.Files.Domain.Cads.Enums;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Files.Domain.Cads.ValueObjects;

public record CadSorting(
	CadSortingType Type = CadSortingType.CreationDate,
	SortingDirection Direction = SortingDirection.Descending
);
