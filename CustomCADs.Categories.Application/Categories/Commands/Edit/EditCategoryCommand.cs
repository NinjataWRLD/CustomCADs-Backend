namespace CustomCADs.Categories.Application.Categories.Commands.Edit;

public sealed record EditCategoryCommand(
    CategoryId Id,
    CategoryWriteDto Dto
) : ICommand;
