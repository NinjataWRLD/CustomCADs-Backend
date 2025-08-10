namespace CustomCADs.Shared.Application.UseCases.Categories.Queries;

public record GetCategoryExistsByIdQuery(CategoryId Id) : IQuery<bool>;
