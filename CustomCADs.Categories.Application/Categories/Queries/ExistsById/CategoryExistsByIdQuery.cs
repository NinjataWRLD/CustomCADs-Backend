namespace CustomCADs.Categories.Application.Categories.Queries.ExistsById;

public record CategoryExistsByIdQuery(CategoryId Id) : IQuery<bool>;
