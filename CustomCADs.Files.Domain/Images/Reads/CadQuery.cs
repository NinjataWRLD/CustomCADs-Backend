using CustomCADs.Files.Domain.Images.ValueObjects;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Files.Domain.Images.Reads;

public record ImageQuery(
    Pagination Pagination,
    ImageSorting? Sorting = null
);
