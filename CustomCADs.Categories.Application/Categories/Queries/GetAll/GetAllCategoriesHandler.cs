using CustomCADs.Categories.Domain.Categories.Reads;
using CustomCADs.Shared.Application.Cache;

namespace CustomCADs.Categories.Application.Categories.Queries.GetAll;

public sealed class GetAllCategoriesHandler(ICategoryReads reads, ICacheService cache)
    : IQueryHandler<GetAllCategoriesQuery, IEnumerable<CategoryReadDto>>
{
    public async Task<IEnumerable<CategoryReadDto>> Handle(GetAllCategoriesQuery req, CancellationToken ct)
    {
        IEnumerable<Category> categories =
            await cache.GetAsync<IEnumerable<Category>>("categories").ConfigureAwait(false)
            ?? await reads.AllAsync(track: false, ct: ct).ConfigureAwait(false);

        return categories.Select(c => c.ToCategoryReadDto());
    }
}
