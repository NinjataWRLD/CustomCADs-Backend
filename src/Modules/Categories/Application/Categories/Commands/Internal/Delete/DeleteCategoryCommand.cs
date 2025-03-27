namespace CustomCADs.Categories.Application.Categories.Commands.Internal.Delete;

public sealed record DeleteCategoryCommand(
    CategoryId Id
) : ICommand;
