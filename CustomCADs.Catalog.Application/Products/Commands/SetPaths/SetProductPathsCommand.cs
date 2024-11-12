namespace CustomCADs.Catalog.Application.Products.Commands.SetPaths;

public record SetProductPathsCommand(
    ProductId Id,
    string? CadPath = default,
    string? ImagePath = default
) : ICommand;
