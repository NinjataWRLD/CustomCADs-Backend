namespace CustomCADs.Categories.Application.Categories.Commands.Create;

public sealed record CreateCategoryCommand(
    CategoryWriteDto Dto
) : ICommand<CategoryId>;
