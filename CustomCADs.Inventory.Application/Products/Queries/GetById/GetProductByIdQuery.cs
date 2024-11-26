using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Inventory.Application.Products.Queries.GetById;

public record GetProductByIdQuery(
    ProductId Id,
    UserId CreatorId
) : IQuery<GetProductByIdDto>;
