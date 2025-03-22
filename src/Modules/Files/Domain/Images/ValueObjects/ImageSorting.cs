using CustomCADs.Files.Domain.Images.Enums;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Files.Domain.Images.ValueObjects;

public record ImageSorting(
    ImageSortingType Type = ImageSortingType.CreationDate,
    SortingDirection Direction = SortingDirection.Descending
);
