using CustomCADs.Shared.Abstractions.Cache;

namespace CustomCADs.Categories.Application.Common.Caching.Categories;

using static CachingKeys;

public static class CategoriesCachingRemoveExtensions
{
    public static async Task RemoveCategoriesArrayAsync(this ICacheService cache)
        => await cache
            .RemoveAsync<IEnumerable<Category>>(CategoryKey)
            .ConfigureAwait(false);

    public static async Task RemoveCategoryAsync(this ICacheService cache, CategoryId id)
        => await cache
            .RemoveAsync<Category>($"{CategoryKey}/{id}")
            .ConfigureAwait(false);
}
