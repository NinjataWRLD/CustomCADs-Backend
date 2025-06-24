using CustomCADs.Categories.Domain.Repositories.Reads;
using CustomCADs.Shared.UseCases.Categories.Queries;

namespace CustomCADs.Categories.Application.Categories.Queries.Shared;

public sealed class GetCategoryNameByIdHandler(ICategoryReads reads, BaseCachingService<CategoryId, Category> cache)
	: IQueryHandler<GetCategoryNameByIdQuery, string>
{
	public async Task<string> Handle(GetCategoryNameByIdQuery req, CancellationToken ct)
	{
		Category category = await cache.GetOrCreateAsync(
			id: req.Id,
			factory: async () => await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
				?? throw CustomNotFoundException<Category>.ById(req.Id)
		).ConfigureAwait(false);

		return category.Name;
	}
}
