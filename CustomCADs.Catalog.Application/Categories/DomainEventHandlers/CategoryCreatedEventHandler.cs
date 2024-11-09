using CustomCADs.Catalog.Domain.DomainEvents.Categories;
using CustomCADs.Shared.Application.Cache;

namespace CustomCADs.Catalog.Application.Categories.DomainEventHandlers;

public class CategoryCreatedEventHandler(ICacheService cache)
{
    public async Task Handle(CategoryCreatedEvent ccEvent)
    {
        await cache.RemoveAsync<IEnumerable<Category>>("categories").ConfigureAwait(false);
        await cache.SetAsync(
            ($"categories/{ccEvent.Category.Id}", ccEvent.Category)
        ).ConfigureAwait(false);
    }
}
