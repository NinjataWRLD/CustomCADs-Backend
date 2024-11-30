using CustomCADs.Inventory.Domain.Products.Enums;
using CustomCADs.Inventory.Domain.Products.ValueObjects;
using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Inventory.Domain.Products.Reads;

public record ProductQuery(
    AccountId? CreatorId = null,
    ProductStatus? Status = null,
    string? Name = null,
    ProductSorting? Sorting = null,
    int Page = 1,
    int Limit = 20
);