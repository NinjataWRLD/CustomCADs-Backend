namespace CustomCADs.Shared.UseCases.Categories.Queries;

public sealed record GetCategoryNamesByIdsQuery(
    CategoryId[] Ids
) : IQuery<Dictionary<CategoryId, string>>;
