namespace CustomCADs.Shared.UseCases.Products.Queries;

public record GetProductExistsByIdQuery(ProductId Id) : IQuery<bool>;
