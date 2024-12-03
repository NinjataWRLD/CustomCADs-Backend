using CustomCADs.Accounts.Domain.Accounts;
using CustomCADs.Accounts.Domain.Common;
using CustomCADs.Shared.UseCases.Accounts.Commands;

namespace CustomCADs.Accounts.Application.Accounts.SharedCommandHandlers;

public sealed class CreateAccountHandler(IWrites<Account> writes, IUnitOfWork uow)
    : ICommandHandler<CreateAccountCommand, AccountId>
{
    public async Task<AccountId> Handle(CreateAccountCommand req, CancellationToken ct)
    {
        var account = Account.Create(
            req.Role,
            req.Username,
            req.Email,
            req.TimeZone,
            req.FirstName,
            req.LastName
        );

        await writes.AddAsync(account, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return account.Id;
    }
}
