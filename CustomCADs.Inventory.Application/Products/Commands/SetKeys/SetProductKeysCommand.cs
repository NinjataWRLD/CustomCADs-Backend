using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Inventory;

namespace CustomCADs.Inventory.Application.Products.Commands.SetKeys;

public record SetProductKeysCommand(
    ProductId Id,
    string? CadKey = default,
    string? ImageKey = default
) : ICommand;
