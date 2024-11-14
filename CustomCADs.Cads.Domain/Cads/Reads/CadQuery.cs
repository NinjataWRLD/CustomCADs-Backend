using CustomCADs.Cads.Domain.Cads.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Cads.Domain.Cads.Reads;

public record CadQuery(
    UserId? ClientId = null,
    CadSorting? Sorting = null,
    int Page = 1,
    int Limit = 20
);
