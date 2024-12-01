using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Gallery.Application.Carts.Queries.GetCadUrlGet;

public record GetCartItemCadPresignedUrlGetQuery(
    CartId Id,
    CartItemId ItemId,
    AccountId BuyerId
) : IQuery<GetCartItemCadPresignedUrlGetDto>;
