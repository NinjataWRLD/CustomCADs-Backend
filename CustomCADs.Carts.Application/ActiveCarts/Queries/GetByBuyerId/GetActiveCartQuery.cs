using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.ActiveCarts.Queries.GetByBuyerId;

public sealed record GetActiveCartQuery(
    AccountId BuyerId
) : IQuery<GetActiveCartDto>;
