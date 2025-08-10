using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.UseCases.Categories.Queries;

namespace CustomCADs.Catalog.Application.Categories.Queries.Shared;

public sealed class GetCategoryNamesByIdsHandler(ICategoryReads reads, BaseCachingService<CategoryId, Category> cache)
	: IQueryHandler<GetCategoryNamesByIdsQuery, Dictionary<CategoryId, string>>
{
	public async Task<Dictionary<CategoryId, string>> Handle(GetCategoryNamesByIdsQuery req, CancellationToken ct)
	{
		IEnumerable<Category> categories = await cache.GetOrCreateAsync(
			factory: async () => [.. await reads.AllAsync(track: false, ct: ct).ConfigureAwait(false)]
		).ConfigureAwait(false);

		return categories
			.Where(c => req.Ids.Contains(c.Id))
			.ToDictionary(x => x.Id, x => x.Name);
	}
}
