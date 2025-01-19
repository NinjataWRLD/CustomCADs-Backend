using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Queries.CreatorGetById;

public sealed record CreatorGetProductByIdQuery(
    ProductId Id,
    AccountId CreatorId
) : IQuery<CreatorGetProductByIdDto>;
