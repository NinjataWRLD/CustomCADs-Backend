using CustomCADs.Cads.Domain.Cads.ValueObjects;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Cads.Domain.Cads.Reads;

public record CadQuery(
    Pagination Pagination,
    CadSorting? Sorting = null
);
