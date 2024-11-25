using CustomCADs.Shared.Core.Common.TypedIds.Inventory;

namespace CustomCADs.Inventory.Application.Products.Commands.SetKeys;

public record SetProductKeysCommand(
    ProductId Id,
    string? CadKey = default,
    string? ImageKey = default
) : ICommand;
