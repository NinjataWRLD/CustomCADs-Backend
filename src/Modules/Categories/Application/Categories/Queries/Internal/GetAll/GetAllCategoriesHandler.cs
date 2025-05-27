using CustomCADs.Categories.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Cache;

namespace CustomCADs.Categories.Application.Categories.Queries.Internal.GetAll;

public sealed class GetAllCategoriesHandler(ICategoryReads reads, ICacheService cache)
	: IQueryHandler<GetAllCategoriesQuery, IEnumerable<CategoryReadDto>>
{
	public async Task<IEnumerable<CategoryReadDto>> Handle(GetAllCategoriesQuery req, CancellationToken ct)
	{
		IEnumerable<Category>? categories = await cache.GetCategoriesArrayAsync().ConfigureAwait(false);

		if (categories is null)
		{
			categories = await reads.AllAsync(track: false, ct: ct).ConfigureAwait(false);
			await cache.SetCategoriesArrayAsync(categories).ConfigureAwait(false);
		}

		return categories.Select(c => c.ToDto());
	}
}
