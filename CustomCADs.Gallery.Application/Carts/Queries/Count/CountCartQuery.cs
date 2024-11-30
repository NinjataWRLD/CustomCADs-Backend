using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Gallery.Application.Carts.Queries.Count;

public record CountCartQuery(AccountId BuyerId) : IQuery<int>;
