using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Catalog.Application.Products.Commands.SetPaths;

public record SetProductPathsCommand(
    ProductId Id,
    string? CadPath = default,
    string? ImagePath = default
) : ICommand;
