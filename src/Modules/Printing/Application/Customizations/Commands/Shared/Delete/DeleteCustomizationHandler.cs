using CustomCADs.Printing.Domain.Repositories;
using CustomCADs.Printing.Domain.Repositories.Reads;
using CustomCADs.Shared.UseCases.Customizations.Commands;

namespace CustomCADs.Printing.Application.Customizations.Commands.Shared.Delete;

public class DeleteCustomizationHandler(ICustomizationReads reads, IWrites<Customization> writes, IUnitOfWork uow)
	: ICommandHandler<DeleteCustomizationByIdCommand>
{
	public async Task Handle(DeleteCustomizationByIdCommand req, CancellationToken ct)
	{
		Customization customization = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Customization>.ById(req.Id);

		writes.Remove(customization);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);
	}
}
