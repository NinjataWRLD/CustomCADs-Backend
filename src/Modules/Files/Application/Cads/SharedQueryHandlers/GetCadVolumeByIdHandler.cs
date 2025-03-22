using CustomCADs.Files.Application.Common.Exceptions;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Queries;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.Files.Application.Cads.SharedQueryHandlers;

public class GetCadVolumeByIdHandler(ICadReads reads)
    : IQueryHandler<GetCadVolumeByIdQuery, decimal>
{
    public async Task<decimal> Handle(GetCadVolumeByIdQuery req, CancellationToken ct)
    {
        Cad cad = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CadNotFoundException.ById(req.Id);

        return cad.Volume;
    }
}
