using CustomCADs.Accounts.Application.Common.Exceptions;
using CustomCADs.Accounts.Domain.Repositories;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.ApplicationEvents.Catalog;

namespace CustomCADs.Accounts.Application.Accounts.ApplicationEventHandlers;

public class UserViewedProductHandler(IAccountReads reads, IUnitOfWork uow)
{
    public async Task Handle(UserViewedProductApplicationEvent ae)
    {
        Account account = await reads.SingleByIdAsync(ae.AccountId).ConfigureAwait(false)
            ?? throw AccountNotFoundException.ById(ae.AccountId);

        account.AddViewedProduct(ae.Id);
        await uow.SaveChangesAsync().ConfigureAwait(false);
    }
}
