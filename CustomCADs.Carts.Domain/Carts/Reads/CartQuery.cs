using CustomCADs.Carts.Domain.Carts.ValueObjects;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Domain.Carts.Reads;

public record CartQuery(
    Pagination Pagination,
    AccountId? BuyerId = null,
    ProductId? ProductId = null,
    CartSorting? Sorting = null
);
