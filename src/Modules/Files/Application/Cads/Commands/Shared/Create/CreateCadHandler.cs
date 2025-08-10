using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Shared.Application.Abstractions.Requests.Commands;
using CustomCADs.Shared.Application.UseCases.Cads.Commands;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.Files.Application.Cads.Commands.Shared.Create;

public sealed class CreateCadHandler(IWrites<Cad> writes, IUnitOfWork uow)
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

		return cad.Id;
	}
}
