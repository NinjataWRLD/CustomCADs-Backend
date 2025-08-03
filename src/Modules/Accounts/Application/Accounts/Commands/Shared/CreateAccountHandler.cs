using CustomCADs.Accounts.Domain.Repositories;
using CustomCADs.Accounts.Domain.Repositories.Writes;
using CustomCADs.Shared.UseCases.Accounts.Commands;

namespace CustomCADs.Accounts.Application.Accounts.Commands.Shared;

public sealed class CreateAccountHandler(IAccountWrites writes, IUnitOfWork uow)
	: ICommandHandler<CreateAccountCommand, AccountId>
{
	public async Task<AccountId> Handle(CreateAccountCommand req, CancellationToken ct)
	{
		Account account = await writes.AddAsync(
			entity: Account.Create(
				role: req.Role,
				username: req.Username,
				email: req.Email,
				firstName: req.FirstName,
				lastName: req.LastName
			),
			ct
		).ConfigureAwait(false);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		return account.Id;
	}
}
