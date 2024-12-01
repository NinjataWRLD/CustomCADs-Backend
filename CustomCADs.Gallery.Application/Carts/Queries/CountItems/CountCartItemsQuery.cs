using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Gallery.Application.Carts.Queries.CountItems;

public record CountCartItemsQuery(AccountId BuyerId) : IQuery<Dictionary<CartId, int>>;
