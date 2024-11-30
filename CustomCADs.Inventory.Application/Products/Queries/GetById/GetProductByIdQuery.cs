using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Inventory.Application.Products.Queries.GetById;

public record GetProductByIdQuery(
    ProductId Id,
    AccountId CreatorId
) : IQuery<GetProductByIdDto>;
