using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Designer.Report;

public sealed class ReportCustomHandler(ICustomReads reads, IUnitOfWork uow)
	: ICommandHandler<ReportCustomCommand>
{
	public async Task Handle(ReportCustomCommand req, CancellationToken ct)
	{
		Custom custom = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Custom>.ById(req.Id);

		if (custom.AcceptedCustom?.DesignerId != req.DesignerId)
		{
			throw CustomAuthorizationException<Custom>.ById(req.Id);
		}

		custom.Report();
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);
	}
}

