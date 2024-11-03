using CustomCADs.Catalog.Application.Common.Contracts;

namespace CustomCADs.Catalog.Application.Categories.Commands.Create;

public record CreateCategoryCommand(CategoryWriteDto Dto) : ICommand<int>;
