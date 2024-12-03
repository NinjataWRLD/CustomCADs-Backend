using CustomCADs.Carts.Domain.Carts.ValueObjects;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Domain.Carts.Reads;

public record CartQuery(
    AccountId? BuyerId = null,
    CartSorting? Sorting = null,
    int Page = 1,
    int Limit = 20
);
