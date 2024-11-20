namespace CustomCADs.Shared.UseCases.Products.Queries;

public record GetProductPriceByIdQuery(ProductId Id) : IQuery<decimal>;