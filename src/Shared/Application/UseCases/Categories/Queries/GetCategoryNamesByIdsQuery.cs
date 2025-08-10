namespace CustomCADs.Shared.Application.UseCases.Categories.Queries;

public sealed record GetCategoryNamesByIdsQuery(
	CategoryId[] Ids
) : IQuery<Dictionary<CategoryId, string>>;
