using CustomCADs.Catalog.Application.Common.Contracts;

namespace CustomCADs.Catalog.Application.Products.Commands.SetPaths;

public record SetProductPathsCommand(
    Guid Id, 
    string? CadPath = default, 
    string? ImagePath = default
) : ICommand;
