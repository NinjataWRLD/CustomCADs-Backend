using CustomCADs.Catalog.Domain.Products.Events;
using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.ApplicationEvents.Catalog;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Catalog.Application.Products.DomainEventHandlers;

public class ProductViewedHandler(IProductReads reads, IUnitOfWork uow, IRequestSender sender, IEventRaiser raiser)
{
    public async Task Handle(ProductViewedDomainEvent de)
    {
        Product product = await reads.SingleByIdAsync(de.Id).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Product>.ById(de.Id);

        GetAccountViewedProductQuery userAlreadyViewedQuery = new(de.AccountId, de.Id);
        bool userAlreadyViewed = await sender.SendQueryAsync(userAlreadyViewedQuery).ConfigureAwait(false);
        if (userAlreadyViewed) return;

        product.AddToViewCount();
        await uow.SaveChangesAsync().ConfigureAwait(false);

        await raiser.RaiseApplicationEventAsync(new UserViewedProductApplicationEvent(
            Id: de.Id,
            AccountId: de.AccountId
        )).ConfigureAwait(false);
    }
}
