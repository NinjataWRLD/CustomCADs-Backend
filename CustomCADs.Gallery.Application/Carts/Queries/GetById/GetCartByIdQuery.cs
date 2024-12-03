using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Gallery.Application.Carts.Queries.GetById;

public sealed record GetCartByIdQuery(
    CartId Id,
    AccountId BuyerId
) : IQuery<GetCartByIdDto>;
