using CustomCADs.Shared.Application.Requests.Queries;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Cads;

namespace CustomCADs.Shared.Queries.Cads;

public record GetCadIdByPathQuery(string Path) : IQuery<CadId>;