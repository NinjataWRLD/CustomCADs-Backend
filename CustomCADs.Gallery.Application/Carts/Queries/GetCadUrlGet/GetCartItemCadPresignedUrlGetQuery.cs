using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Gallery.Application.Carts.Queries.GetCadUrlGet;

public sealed record GetCartItemCadPresignedUrlGetQuery(
    CartId Id,
    CartItemId ItemId,
    AccountId BuyerId
) : IQuery<GetCartItemCadPresignedUrlGetDto>;
