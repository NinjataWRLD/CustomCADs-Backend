using CustomCADs.Inventory.Domain.Products.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Inventory.Application.Products.Queries.Count;

public record ProductsCountQuery(
    UserId CreatorId,
    ProductStatus Status
) : IQuery<int>;
