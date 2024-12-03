using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Inventory.Application.Products.Queries.GetById;

public sealed record GetProductByIdQuery(
    ProductId Id,
    AccountId CreatorId
) : IQuery<GetProductByIdDto>;
