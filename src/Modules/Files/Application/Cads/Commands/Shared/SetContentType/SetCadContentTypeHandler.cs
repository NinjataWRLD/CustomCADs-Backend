using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Commands;
using CustomCADs.Shared.UseCases.Cads.Commands;

namespace CustomCADs.Files.Application.Cads.Commands.Shared.SetContentType;

public sealed class SetCadContentTypeHandler(ICadReads reads, IUnitOfWork uow)
	: ICommandHandler<SetCadContentTypeCommand>
{
	public async Task Handle(SetCadContentTypeCommand req, CancellationToken ct = default)
	{
		Cad cad = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Cad>.ById(req.Id);

		cad.SetContentType(req.ContentType);

		await uow.SaveChangesAsync(ct).ConfigureAwait(false);
	}
}
