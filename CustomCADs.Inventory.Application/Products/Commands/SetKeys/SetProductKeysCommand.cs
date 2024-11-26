using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Inventory.Application.Products.Commands.SetKeys;

public record SetProductKeysCommand(
    ProductId Id,
    UserId CreatorId,
    string? CadKey = default,
    string? ImageKey = default
) : ICommand;
