namespace CustomCADs.Catalog.Application.Products.Commands.SetKeys;

public record SetProductKeysCommand(
    ProductId Id,
    string? CadKey = default,
    string? ImageKey = default
) : ICommand;
