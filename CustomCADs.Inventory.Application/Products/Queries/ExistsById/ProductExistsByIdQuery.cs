namespace CustomCADs.Inventory.Application.Products.Queries.ExistsById;

public record ProductExistsByIdQuery(ProductId Id) : IQuery<bool>;