using CustomCADs.Accounts.Domain.Repositories;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.ApplicationEvents.Account.Accounts;

namespace CustomCADs.Accounts.Application.Accounts.Commands.Internal.Create;

public sealed class CreateAccountHandler(IWrites<Account> writes, IUnitOfWork uow, IEventRaiser raiser)
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

		await raiser.RaiseApplicationEventAsync(new AccountCreatedApplicationEvent(
			Id: account.Id,
			Role: account.RoleName,
			Username: account.Username,
			Email: account.Email,
			Password: req.Password
		)).ConfigureAwait(false);

		return account.Id;
	}
}
