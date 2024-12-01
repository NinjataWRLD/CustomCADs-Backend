using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Gallery.Application.Carts.Queries.GetItems;

public record GetCartItemsByIdQuery(
    CartId Id,
    AccountId BuyerId
) : IQuery<ICollection<GetCartItemsByIdDto>>;
