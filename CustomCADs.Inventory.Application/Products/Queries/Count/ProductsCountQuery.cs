using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Inventory.Application.Products.Queries.Count;

public sealed record ProductsCountQuery(
    AccountId CreatorId
) : IQuery<ProductsCountDto>;
