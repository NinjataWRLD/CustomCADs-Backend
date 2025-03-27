using CustomCADs.Categories.Domain.Categories.Events;
using CustomCADs.Shared.Abstractions.Cache;

namespace CustomCADs.Categories.Application.Categories.EventHandlers.Domain;

public class CategoryEditedEventHandler(ICacheService cache)
{
    public async Task Handle(CategoryEditedDomainEvent de)
    {
        await cache.RemoveCategoriesArrayAsync().ConfigureAwait(false);
        await cache.SetCategoryAsync(de.Id, de.Category).ConfigureAwait(false);
    }
}
