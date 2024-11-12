namespace CustomCADs.Catalog.Application.Products.Queries.GetById;

public record GetProductByIdQuery(ProductId Id) : IQuery<GetProductByIdDto>;
