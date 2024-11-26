namespace CustomCADs.Shared.UseCases.Categories.Queries;

public record GetCategoriesByIdsQuery(params CategoryId[] Ids)
    : IQuery<IEnumerable<(CategoryId Id, string Name)>>;
