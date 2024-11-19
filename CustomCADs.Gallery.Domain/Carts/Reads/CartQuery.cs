using CustomCADs.Gallery.Domain.Carts.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Gallery.Domain.Carts.Reads;

public record CartQuery(
    UserId? BuyerId = null,
    CartSorting? Sorting = null,
    int Page = 1,
    int Limit = 20
);
