using CustomCADs.Catalog.Domain.Repositories.Reads;

namespace CustomCADs.Catalog.Application.Categories.Queries.Internal.GetById;

public sealed class GetCategoryByIdHandler(ICategoryReads reads, BaseCachingService<CategoryId, Category> cache)
	: IQueryHandler<GetCategoryByIdQuery, CategoryReadDto>
{
	public async Task<CategoryReadDto> Handle(GetCategoryByIdQuery req, CancellationToken ct)
	{
		Category category = await cache.GetOrCreateAsync(
			id: req.Id,
			factory: async () => await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
				?? throw CustomNotFoundException<Category>.ById(req.Id)
		).ConfigureAwait(false);

		return category.ToDto();
	}
}
