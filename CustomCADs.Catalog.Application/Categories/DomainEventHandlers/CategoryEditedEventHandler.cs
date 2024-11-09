using CustomCADs.Catalog.Domain.DomainEvents.Categories;
using CustomCADs.Shared.Application.Cache;

namespace CustomCADs.Catalog.Application.Categories.DomainEventHandlers;

public class CategoryEditedEventHandler(ICacheService cache)
{
    public async Task Handle(CategoryEditedEvent ceEvent)
    {
        await cache.RemoveAsync<IEnumerable<Category>>($"categories").ConfigureAwait(false);
        await cache.SetAsync(($"categories/{ceEvent.Id}", ceEvent.Category)).ConfigureAwait(false);
    }
}
