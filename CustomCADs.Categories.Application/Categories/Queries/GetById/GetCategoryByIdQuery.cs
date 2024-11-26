namespace CustomCADs.Categories.Application.Categories.Queries.GetById;

public record GetCategoryByIdQuery(CategoryId Id) : IQuery<CategoryReadDto>;
