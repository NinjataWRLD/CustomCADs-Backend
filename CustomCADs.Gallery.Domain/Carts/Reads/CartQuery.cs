using CustomCADs.Gallery.Domain.Carts.ValueObjects;
using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Gallery.Domain.Carts.Reads;

public record CartQuery(
    AccountId? BuyerId = null,
    CartSorting? Sorting = null,
    int Page = 1,
    int Limit = 20
);
