namespace CustomCADs.Shared.UseCases.Categories.Queries;

public record GetCategoryNameByIdQuery(CategoryId Id) : IQuery<string>;
