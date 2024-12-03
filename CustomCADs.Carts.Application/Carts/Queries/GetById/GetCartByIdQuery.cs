using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.Carts.Queries.GetById;

public sealed record GetCartByIdQuery(
    CartId Id,
    AccountId BuyerId
) : IQuery<GetCartByIdDto>;
