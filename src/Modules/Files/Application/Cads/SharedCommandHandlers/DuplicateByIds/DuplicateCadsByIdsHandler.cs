using CustomCADs.Files.Domain.Cads.Reads;
using CustomCADs.Files.Domain.Common;
using CustomCADs.Shared.Abstractions.Requests.Commands;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Cads.Commands;

namespace CustomCADs.Files.Application.Cads.SharedCommandHandlers.DuplicateByIds;

public class DuplicateCadsByIdsHandler(ICadReads reads, IWrites<Cad> writes, IUnitOfWork uow)
    : ICommandHandler<DuplicateCadsByIdsCommand, Dictionary<CadId, CadId>>
{
    public async Task<Dictionary<CadId, CadId>> Handle(DuplicateCadsByIdsCommand req, CancellationToken ct)
    {
        CadQuery query = new(
            Pagination: new(1, req.Ids.Length),
            Ids: req.Ids
        );
        Result<Cad> result = await reads.AllAsync(query, track: false, ct).ConfigureAwait(false);

        Dictionary<Cad, Cad> newCads = [];
        foreach (Cad cad in result.Items)
        {
            Cad newCad = Cad.Create(
                key: cad.Key,
                contentType: cad.ContentType,
                camCoordinates: cad.CamCoordinates,
                panCoordinates: cad.PanCoordinates
            );
            await writes.AddAsync(newCad, ct).ConfigureAwait(false);
            newCads[cad] = newCad;
        }
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return newCads.ToDictionary(x => x.Key.Id, x => x.Value.Id);
    }
}
