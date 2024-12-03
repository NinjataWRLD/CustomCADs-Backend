namespace CustomCADs.Shared.UseCases.Categories.Queries;

public sealed record GetCategoriesByIdsQuery(
    CategoryId[] Ids
) : IQuery<IEnumerable<(CategoryId Id, string Name)>>;
