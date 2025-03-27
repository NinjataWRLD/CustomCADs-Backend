namespace CustomCADs.Categories.Application.Categories.Queries.Internal.GetById;

public sealed record GetCategoryByIdQuery(
    CategoryId Id
) : IQuery<CategoryReadDto>;
