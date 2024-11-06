namespace CustomCADs.Catalog.Application.Categories.Commands.Edit;

public record EditCategoryCommand(int Id, CategoryWriteDto Dto) : ICommand;
