using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Create;

public sealed class CreateCustomHandler(IWrites<Custom> writes, IUnitOfWork uow, IRequestSender sender)
	: ICommandHandler<CreateCustomCommand, CustomId>
{
	public async Task<CustomId> Handle(CreateCustomCommand req, CancellationToken ct)
	{
		if (!await sender.SendQueryAsync(new GetAccountExistsByIdQuery(req.BuyerId), ct).ConfigureAwait(false))
		{
			throw CustomNotFoundException<Custom>.ById(req.BuyerId, "User");
		}

		Custom order = req.ToEntity();

		await writes.AddAsync(order, ct).ConfigureAwait(false);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		return order.Id;
	}
}
