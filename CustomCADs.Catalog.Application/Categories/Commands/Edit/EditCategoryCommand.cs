using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Catalog.Application.Categories.Commands.Edit;

public record EditCategoryCommand(CategoryId Id, CategoryWriteDto Dto) : ICommand;
