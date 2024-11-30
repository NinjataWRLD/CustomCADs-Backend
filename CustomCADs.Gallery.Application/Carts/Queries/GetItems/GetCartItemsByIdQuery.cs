using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Gallery.Application.Carts.Queries.GetItems;

public record GetCartItemsByIdQuery(
    CartId Id,
    AccountId BuyerId
) : IQuery<ICollection<GetCartItemsByIdDto>>;
