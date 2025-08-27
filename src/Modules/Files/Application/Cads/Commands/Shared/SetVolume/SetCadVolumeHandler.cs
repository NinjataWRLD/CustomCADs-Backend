using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Commands;
using CustomCADs.Shared.Application.UseCases.Cads.Commands;

namespace CustomCADs.Files.Application.Cads.Commands.Shared.SetVolume;

public sealed class SetCadVolumeHandler(ICadReads reads, IUnitOfWork uow, BaseCachingService<CadId, Cad> cache)
	: ICommandHandler<SetCadVolumeCommand>
{
	public async Task Handle(SetCadVolumeCommand req, CancellationToken ct = default)
	{
		Cad cad = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Cad>.ById(req.Id);

		cad.SetVolume(req.Volume);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		await cache.UpdateAsync(cad.Id, cad).ConfigureAwait(false);
	}
}
