namespace CustomCADs.Catalog.Application.Categories.Commands.Delete;

public record DeleteCategoryCommand(CategoryId Id) : ICommand;
