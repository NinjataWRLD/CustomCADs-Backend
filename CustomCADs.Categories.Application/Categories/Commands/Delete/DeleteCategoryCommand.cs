using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.Categories.Application.Categories.Commands.Delete;

public record DeleteCategoryCommand(CategoryId Id) : ICommand;
