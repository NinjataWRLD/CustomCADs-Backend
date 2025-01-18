using CustomCADs.Shared.Abstractions.Cache;

namespace CustomCADs.Categories.Application.Common.Caching.Categories;

using static CachingKeys;

public static class CategoriesCachingGetExtensions
{
    public static async Task<IEnumerable<Category>?> GetCategoriesArrayAsync(this ICacheService cache)
        => await cache
            .GetAsync<IEnumerable<Category>>(CategoryKey)
            .ConfigureAwait(false);

    public static async Task<Category?> GetCategoryAsync(this ICacheService cache, CategoryId id)
        => await cache
            .GetAsync<Category>($"{CategoryKey}/{id}")
            .ConfigureAwait(false);
}
