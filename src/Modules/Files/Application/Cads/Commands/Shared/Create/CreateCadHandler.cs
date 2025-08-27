using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Shared.Application.Abstractions.Requests.Commands;
using CustomCADs.Shared.Application.UseCases.Cads.Commands;

namespace CustomCADs.Files.Application.Cads.Commands.Shared.Create;

public sealed class CreateCadHandler(IWrites<Cad> writes, IUnitOfWork uow, BaseCachingService<CadId, Cad> cache)
	: ICommandHandler<CreateCadCommand, CadId>
{
	public async Task<CadId> Handle(CreateCadCommand req, CancellationToken ct)
	{
		Cad cad = await writes.AddAsync(
			entity: Cad.Create(
				key: req.Key,
				contentType: req.ContentType,
				volume: req.Volume,
				camCoordinates: new(),
				panCoordinates: new()
			),
			ct
		).ConfigureAwait(false);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await cache.UpdateAsync(cad.Id, cad).ConfigureAwait(false);

		return cad.Id;
	}
}
