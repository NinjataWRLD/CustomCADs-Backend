using CustomCADs.Catalog.Application.Common.Contracts;

namespace CustomCADs.Catalog.Application.Categories.Commands.Delete;

public record DeleteCategoryCommand(int Id) : ICommand;
