using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Gallery.Application.Carts.Queries.Count;

public record CountCartQuery(UserId BuyerId) : IQuery<int>;
