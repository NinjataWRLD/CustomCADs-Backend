using CustomCADs.Shared.Application.Cache;

namespace CustomCADs.Categories.Application.Common.Caching.Categories;

using static CachingKeys;

public static class CategoriesCachingSetExtensions
{
    public static async Task SetCategoriesArrayAsync(this ICacheService cache, params IEnumerable<Category> categories)
        => await cache
            .SetAsync(CategoryKey, categories)
            .ConfigureAwait(false);

    public static async Task SetCategoryAsync(this ICacheService cache, CategoryId id, Category category)
        => await cache
            .SetAsync($"{CategoryKey}/{id}", category)
            .ConfigureAwait(false);
}
