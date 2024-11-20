using CustomCADs.Cads.Domain.Cads.ValueObjects;

namespace CustomCADs.Cads.Domain.Cads.Reads;

public record CadQuery(
    CadSorting? Sorting = null,
    int Page = 1,
    int Limit = 20
);
