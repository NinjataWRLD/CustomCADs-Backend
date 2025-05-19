using CustomCADs.Accounts.Domain.Repositories;
using CustomCADs.Shared.UseCases.Accounts.Commands;

namespace CustomCADs.Accounts.Application.Accounts.Commands.Shared;

public sealed class CreateAccountHandler(IWrites<Account> writes, IUnitOfWork uow)
	: ICommandHandler<CreateAccountCommand, AccountId>
{
	public async Task<AccountId> Handle(CreateAccountCommand req, CancellationToken ct)
	{
		var account = Account.Create(
			role: req.Role,
			username: req.Username,
			email: req.Email,
			firstName: req.FirstName,
			lastName: req.LastName
		);

		await writes.AddAsync(account, ct).ConfigureAwait(false);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		return account.Id;
	}
}
