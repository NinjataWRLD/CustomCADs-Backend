using CustomCADs.Accounts.Application.Common.Exceptions;
using CustomCADs.Accounts.Domain.Accounts.Reads;
using CustomCADs.Accounts.Domain.Common;
using CustomCADs.Shared.IntegrationEvents.Catalog;

namespace CustomCADs.Accounts.Application.Accounts.IntegrationEventHandlers;

public class UserViewedProductHandler(IAccountReads reads, IUnitOfWork uow)
{
    public async Task Handle(UserViewedProductIntegrationEvent ie)
    {
        Account account = await reads.SingleByIdAsync(ie.AccountId).ConfigureAwait(false)
            ?? throw AccountNotFoundException.ById(ie.AccountId);

        account.AddViewedProduct(ie.Id);
        await uow.SaveChangesAsync().ConfigureAwait(false);
    }
}
