using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Catalog.Application.Products.Queries.GetById;

public record GetProductByIdQuery(ProductId Id) : IQuery<GetProductByIdDto>;
