using CustomCADs.Catalog.Domain.DomainEvents.Categories;
using CustomCADs.Shared.Application.Cache;

namespace CustomCADs.Catalog.Application.Categories.DomainEventHandlers;

public class CategoryDeletedEventHandler(ICacheService cache)
{
    public async Task Handle(CategoryDeletedEvent cdEvent)
    {
        await cache.RemoveAsync<IEnumerable<Category>>($"categories").ConfigureAwait(false);
        await cache.RemoveAsync<Category>($"categories/{cdEvent.Id}").ConfigureAwait(false);
    }
}
