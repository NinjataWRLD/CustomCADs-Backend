using CustomCADs.Cads.Domain.Cads.Entites;
using CustomCADs.Cads.Domain.Cads.Reads;
using CustomCADs.Cads.Domain.Common.Exceptions.Cads;
using CustomCADs.Shared.Application.Requests.Queries;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Cads;
using CustomCADs.Shared.Queries.Cads;

namespace CustomCADs.Cads.Application.Cads.SharedQueries;

public class GetCadIdByPathHandler(ICadReads reads)
    : IQueryHandler<GetCadIdByPathQuery, CadId>
{
    public async Task<CadId> Handle(GetCadIdByPathQuery req, CancellationToken ct)
    {
        Cad cad = await reads.SingleByPathAsync(req.Path, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CadNotFoundException.ByPath(req.Path);

        return cad.Id;
    }
}
