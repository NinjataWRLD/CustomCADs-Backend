using CustomCADs.Categories.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Cache;

namespace CustomCADs.Categories.Application.Categories.Queries.Internal.GetById;

public sealed class GetCategoryByIdHandler(ICategoryReads reads, ICacheService cache)
	: IQueryHandler<GetCategoryByIdQuery, CategoryReadDto>
{
	public async Task<CategoryReadDto> Handle(GetCategoryByIdQuery req, CancellationToken ct)
	{
		Category? category = await cache.GetCategoryAsync(req.Id).ConfigureAwait(false);

		if (category is null)
		{
			category = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
				?? throw CustomNotFoundException<Category>.ById(req.Id);

			await cache.SetCategoryAsync(category.Id, category).ConfigureAwait(false);
		}

		return category.ToDto();
	}
}
