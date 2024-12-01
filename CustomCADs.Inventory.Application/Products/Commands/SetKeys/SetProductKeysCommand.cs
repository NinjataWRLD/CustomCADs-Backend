﻿using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Inventory.Application.Products.Commands.SetKeys;

public record SetProductKeysCommand(
    ProductId Id,
    AccountId CreatorId,
    string? CadKey = default,
    string? ImageKey = default
) : ICommand;
