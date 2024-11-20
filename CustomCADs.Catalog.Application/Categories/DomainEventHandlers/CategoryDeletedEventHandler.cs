using CustomCADs.Catalog.Domain.Categories;
using CustomCADs.Catalog.Domain.Categories.DomainEvents;
using CustomCADs.Shared.Application.Cache;

namespace CustomCADs.Catalog.Application.Categories.DomainEventHandlers;

public class CategoryDeletedEventHandler(ICacheService cache)
{
    public async Task Handle(CategoryDeletedDomainEvent de)
    {
        await cache.RemoveAsync<IEnumerable<Category>>($"categories").ConfigureAwait(false);
        await cache.RemoveAsync<Category>($"categories/{de.Id}").ConfigureAwait(false);
    }
}
