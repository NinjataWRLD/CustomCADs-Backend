using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetById;

public sealed record CreatorGetProductByIdQuery(
	ProductId Id,
	AccountId CreatorId
) : IQuery<CreatorGetProductByIdDto>;
