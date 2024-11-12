namespace CustomCADs.Catalog.Application.Products.Queries.GetProductAndAdjacentById;

public record GetProductAndAdjacentByIdQuery(ProductId Id) : IQuery<GetProductAndAdjacentByIdDto>;
