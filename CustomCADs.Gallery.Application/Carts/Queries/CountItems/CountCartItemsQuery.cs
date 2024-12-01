﻿using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Gallery.Application.Carts.Queries.CountItems;

public record CountCartItemsQuery(AccountId BuyerId) : IQuery<Dictionary<CartId, int>>;
