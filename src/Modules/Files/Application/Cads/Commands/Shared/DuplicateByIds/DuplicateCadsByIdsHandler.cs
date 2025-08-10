using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Commands;
using CustomCADs.Shared.Application.UseCases.Cads.Commands;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.Files.Application.Cads.Commands.Shared.DuplicateByIds;

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
				volume: cad.Volume,
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
