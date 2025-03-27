using CustomCADs.Categories.Domain.Categories.Events;
using CustomCADs.Shared.Abstractions.Cache;

namespace CustomCADs.Categories.Application.Categories.EventHandlers.Domain;

public class CategoryDeletedEventHandler(ICacheService cache)
{
    public async Task Handle(CategoryDeletedDomainEvent de)
    {
        await cache.RemoveCategoriesArrayAsync().ConfigureAwait(false);
        await cache.RemoveCategoryAsync(de.Id).ConfigureAwait(false);
    }
}
