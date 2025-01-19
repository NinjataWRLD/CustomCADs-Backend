namespace CustomCADs.Shared.UseCases.Categories.Queries;

public record GetCategoryExistsByIdQuery(CategoryId Id) : IQuery<bool>;
