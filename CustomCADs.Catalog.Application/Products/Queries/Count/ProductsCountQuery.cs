using CustomCADs.Catalog.Domain.Products.Enums;
using MediatR;

namespace CustomCADs.Catalog.Application.Products.Queries.Count;

public record ProductsCountQuery(Guid CreatorId, ProductStatus Status) : IRequest<int>;
