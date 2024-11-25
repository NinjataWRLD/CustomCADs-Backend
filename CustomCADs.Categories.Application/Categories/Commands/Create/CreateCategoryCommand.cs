using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.Categories.Application.Categories.Commands.Create;

public record CreateCategoryCommand(CategoryWriteDto Dto) : ICommand<CategoryId>;
