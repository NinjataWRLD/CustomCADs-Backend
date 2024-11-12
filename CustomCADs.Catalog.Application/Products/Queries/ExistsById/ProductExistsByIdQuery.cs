namespace CustomCADs.Catalog.Application.Products.Queries.ExistsById;

public record ProductExistsByIdQuery(ProductId Id) : IQuery<bool>;