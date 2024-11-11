using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Catalog.Application.Products.Queries.GetProductAndAdjacentById;

public record GetProductAndAdjacentByIdQuery(ProductId Id) : IQuery<GetProductAndAdjacentByIdDto>;
