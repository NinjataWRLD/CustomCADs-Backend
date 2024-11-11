using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Catalog.Application.Categories.Commands.Create;

public record CreateCategoryCommand(CategoryWriteDto Dto) : ICommand<CategoryId>;
