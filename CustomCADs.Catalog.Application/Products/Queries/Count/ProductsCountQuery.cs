using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Queries.Count;

public sealed record ProductsCountQuery(
    AccountId CreatorId
) : IQuery<ProductsCountDto>;
