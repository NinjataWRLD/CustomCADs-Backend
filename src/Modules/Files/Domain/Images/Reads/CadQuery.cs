using CustomCADs.Files.Domain.Images.ValueObjects;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Files.Domain.Images.Reads;

public record ImageQuery(
    Pagination Pagination,
    ImageId[]? Ids = null,
    ImageSorting? Sorting = null
);
