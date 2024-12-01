using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Inventory.Application.Products.Queries.GetById;

public record GetProductByIdQuery(
    ProductId Id,
    AccountId CreatorId
) : IQuery<GetProductByIdDto>;
