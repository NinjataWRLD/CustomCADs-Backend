using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Cads.Commands;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Designer.Finish;

public sealed class FinishCustomHandler(ICustomReads reads, IUnitOfWork uow, IRequestSender sender)
	: ICommandHandler<FinishCustomCommand>
{
	public async Task Handle(FinishCustomCommand req, CancellationToken ct)
	{
		Custom custom = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Custom>.ById(req.Id);

		if (custom.AcceptedCustom?.DesignerId != req.DesignerId)
		{
			throw CustomAuthorizationException<Custom>.ById(req.Id);
		}

		CadId cadId = await sender.SendCommandAsync(
			new CreateCadCommand(
				Key: req.Cad.Key,
				ContentType: req.Cad.ContentType,
				Volume: req.Cad.Volume
			),
			ct
		).ConfigureAwait(false);

		custom.Finish(cadId, req.Price);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);
	}
}
