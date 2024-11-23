﻿using CustomCADs.Inventory.Domain.Products.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Inventory.Domain.Products.Reads;

public record ProductQuery(
    UserId? CreatorId = null,
    string? Status = null,
    string? Name = null,
    ProductSorting? Sorting = null,
    int Page = 1,
    int Limit = 20
);