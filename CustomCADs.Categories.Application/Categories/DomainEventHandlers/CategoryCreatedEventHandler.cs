using CustomCADs.Categories.Domain.Categories.DomainEvents;
using CustomCADs.Shared.Abstractions.Cache;

namespace CustomCADs.Categories.Application.Categories.DomainEventHandlers;

public class CategoryCreatedEventHandler(ICacheService cache)
{
    public async Task Handle(CategoryCreatedDomainEvent de)
    {
        await cache.RemoveCategoriesArrayAsync().ConfigureAwait(false);
        await cache.SetCategoryAsync(de.Category.Id, de.Category).ConfigureAwait(false);
    }
}
