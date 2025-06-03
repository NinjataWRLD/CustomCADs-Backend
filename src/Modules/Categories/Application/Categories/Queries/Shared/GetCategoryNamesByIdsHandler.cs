using CustomCADs.Categories.Domain.Repositories.Reads;
using CustomCADs.Shared.UseCases.Categories.Queries;

namespace CustomCADs.Categories.Application.Categories.Queries.Shared;

public sealed class GetCategoryNamesByIdsHandler(ICategoryReads reads)
	: IQueryHandler<GetCategoryNamesByIdsQuery, Dictionary<CategoryId, string>>
{
	public async Task<Dictionary<CategoryId, string>> Handle(GetCategoryNamesByIdsQuery req, CancellationToken ct)
	{
		IEnumerable<Category> categories = await reads.AllAsync(track: false, ct: ct).ConfigureAwait(false);

		return categories
			.Where(c => req.Ids.Contains(c.Id))
			.ToDictionary(x => x.Id, x => x.Name);
	}
}
