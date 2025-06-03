using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Designer.Begin;

public sealed class BeginCustomHandler(ICustomReads reads, IUnitOfWork uow)
	: ICommandHandler<BeginCustomCommand>
{
	public async Task Handle(BeginCustomCommand req, CancellationToken ct)
	{
		Custom custom = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Custom>.ById(req.Id);

		if (req.DesignerId != custom.AcceptedCustom?.DesignerId)
		{
			throw CustomAuthorizationException<Custom>.ById(req.Id);
		}

		custom.Begin();
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);
	}
}

