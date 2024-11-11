namespace CustomCADs.Catalog.Application.Categories.Queries.ExistsById;

public record CategoryExistsByIdQuery(CategoryId Id) : IQuery<bool>;
