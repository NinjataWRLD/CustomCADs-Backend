using CustomCADs.Categories.Domain.Repositories.Reads;

namespace CustomCADs.Categories.Application.Categories.Queries.Internal.GetAll;

public sealed class GetAllCategoriesHandler(ICategoryReads reads, BaseCachingService<CategoryId, Category> cache)
	: IQueryHandler<GetAllCategoriesQuery, IEnumerable<CategoryReadDto>>
{
	public async Task<IEnumerable<CategoryReadDto>> Handle(GetAllCategoriesQuery req, CancellationToken ct)
	{
		IEnumerable<Category> categories = await cache.GetOrCreateAsync(
			factory: async () => [.. await reads.AllAsync(track: false, ct: ct).ConfigureAwait(false)]
		).ConfigureAwait(false);

		return categories.Select(c => c.ToDto());
	}
}
