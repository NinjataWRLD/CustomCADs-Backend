using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Edit;

public sealed class EditCustomHandler(ICustomReads reads, IUnitOfWork uow)
	: ICommandHandler<EditCustomCommand>
{
	public async Task Handle(EditCustomCommand req, CancellationToken ct)
	{
		Custom custom = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Custom>.ById(req.Id);

		if (custom.BuyerId != req.BuyerId)
		{
			throw CustomAuthorizationException<Custom>.ById(req.Id);
		}

		custom
			.SetName(req.Name)
			.SetDescription(req.Description);

		await uow.SaveChangesAsync(ct).ConfigureAwait(false);
	}
}
