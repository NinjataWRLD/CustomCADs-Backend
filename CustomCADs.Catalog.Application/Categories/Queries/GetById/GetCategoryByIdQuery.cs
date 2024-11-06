namespace CustomCADs.Catalog.Application.Categories.Queries.GetById;

public record GetCategoryByIdQuery(int Id) : IQuery<CategoryReadDto>;
