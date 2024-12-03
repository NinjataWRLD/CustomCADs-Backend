﻿using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Gallery.Application.Carts.Commands.RemoveItem;

public sealed record RemoveCartItemCommand(
    CartId Id,
    CartItemId ItemId,
    AccountId BuyerId
) : ICommand;
