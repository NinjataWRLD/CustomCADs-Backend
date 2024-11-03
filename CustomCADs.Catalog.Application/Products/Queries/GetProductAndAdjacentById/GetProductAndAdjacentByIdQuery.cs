using CustomCADs.Catalog.Application.Common.Contracts;

namespace CustomCADs.Catalog.Application.Products.Queries.GetProductAndAdjacentById;

public record GetProductAndAdjacentByIdQuery(Guid Id) : IQuery<GetProductAndAdjacentByIdDto>;
