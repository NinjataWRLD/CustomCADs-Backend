using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Commands;
using CustomCADs.Shared.Application.UseCases.Cads.Commands;

namespace CustomCADs.Files.Application.Cads.Commands.Shared.SetKey;

public sealed class SetCadKeyHandler(ICadReads reads, IUnitOfWork uow)
	: ICommandHandler<SetCadKeyCommand>
{
	public async Task Handle(SetCadKeyCommand req, CancellationToken ct = default)
	{
		Cad cad = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Cad>.ById(req.Id);

		cad.SetKey(req.Key);

		await uow.SaveChangesAsync(ct).ConfigureAwait(false);
	}
}
