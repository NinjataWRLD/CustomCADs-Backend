using CustomCADs.Categories.Domain.Categories.Events;
using CustomCADs.Shared.Abstractions.Cache;

namespace CustomCADs.Categories.Application.Categories.Events.Domain;

public class CategoryCreatedEventHandler(ICacheService cache)
{
	public async Task Handle(CategoryCreatedDomainEvent de)
	{
		await cache.RemoveCategoriesArrayAsync().ConfigureAwait(false);
		await cache.SetCategoryAsync(de.Category.Id, de.Category).ConfigureAwait(false);
	}
}
