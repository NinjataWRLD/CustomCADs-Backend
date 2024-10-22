using CustomCADs.Shared.Domain;

namespace CustomCADs.Account.Persistence.Repositories;

public class UnitOfWork(AccountContext context) : IUnitOfWork
{
    public async Task SaveChangesAsync()
        => await context.SaveChangesAsync().ConfigureAwait(false);
}
