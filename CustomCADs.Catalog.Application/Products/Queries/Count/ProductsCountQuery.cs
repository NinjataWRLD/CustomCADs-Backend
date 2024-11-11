using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Catalog.Application.Products.Queries.Count;

public record ProductsCountQuery(UserId CreatorId, ProductStatus Status) : IQuery<int>;
