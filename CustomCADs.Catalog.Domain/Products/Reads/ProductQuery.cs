﻿using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Domain.Products.ValueObjects;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.Catalog.Domain.Products.Reads;

public record ProductQuery(
    ProductId[]? Ids = null,
    AccountId? CreatorId = null,
    CategoryId? CategoryId = null,
    ProductStatus? Status = null,
    string? Name = null,
    ProductSorting? Sorting = null,
    int Page = 1,
    int Limit = 20
);