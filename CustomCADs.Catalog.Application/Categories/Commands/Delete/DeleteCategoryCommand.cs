using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Catalog.Application.Categories.Commands.Delete;

public record DeleteCategoryCommand(CategoryId Id) : ICommand;
