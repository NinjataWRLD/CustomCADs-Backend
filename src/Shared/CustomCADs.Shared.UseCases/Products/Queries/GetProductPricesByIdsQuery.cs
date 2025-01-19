namespace CustomCADs.Shared.UseCases.Products.Queries;

public record GetProductPricesByIdsQuery(
    ProductId[] Ids
) : IQuery<Dictionary<ProductId, decimal>>;
