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

		Custom custom = await writes.AddAsync(
			entity: Custom.Create(
				name: req.Name,
				description: req.Description,
				forDelivery: req.ForDelivery,
				buyerId: req.BuyerId
			),
			ct
		).ConfigureAwait(false);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		return custom.Id;
	}
}
