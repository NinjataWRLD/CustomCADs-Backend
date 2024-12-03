using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Gallery.Application.Carts.Queries.Count;

public sealed record CountCartQuery(
    AccountId BuyerId
) : IQuery<int>;
