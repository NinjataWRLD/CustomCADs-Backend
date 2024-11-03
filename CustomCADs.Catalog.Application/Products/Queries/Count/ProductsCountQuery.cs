using CustomCADs.Catalog.Application.Common.Contracts;
using CustomCADs.Catalog.Domain.Products.Enums;

namespace CustomCADs.Catalog.Application.Products.Queries.Count;

public record ProductsCountQuery(Guid CreatorId, ProductStatus Status) : IQuery<int>;
