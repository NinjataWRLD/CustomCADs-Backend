using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Queries.Creator.Count;

public sealed record ProductsCountQuery(
    AccountId CreatorId
) : IQuery<ProductsCountDto>;
