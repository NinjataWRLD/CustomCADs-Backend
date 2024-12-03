namespace CustomCADs.Categories.Application.Categories.Queries.GetById;

public sealed record GetCategoryByIdQuery(
    CategoryId Id
) : IQuery<CategoryReadDto>;
