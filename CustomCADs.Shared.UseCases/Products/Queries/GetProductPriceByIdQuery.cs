namespace CustomCADs.Shared.UseCases.Products.Queries;

public sealed record GetProductPriceByIdQuery(
    ProductId Id
) : IQuery<decimal>;