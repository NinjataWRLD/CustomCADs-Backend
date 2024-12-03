namespace CustomCADs.Categories.Application.Categories.Queries.GetAll;

public sealed record GetAllCategoriesQuery
    : IQuery<IEnumerable<CategoryReadDto>>;
