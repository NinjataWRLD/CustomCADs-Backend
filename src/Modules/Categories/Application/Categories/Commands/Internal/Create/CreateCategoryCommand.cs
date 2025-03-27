namespace CustomCADs.Categories.Application.Categories.Commands.Internal.Create;

public sealed record CreateCategoryCommand(
    CategoryWriteDto Dto
) : ICommand<CategoryId>;
