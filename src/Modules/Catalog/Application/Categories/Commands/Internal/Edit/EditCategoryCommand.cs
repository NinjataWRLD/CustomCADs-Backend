namespace CustomCADs.Catalog.Application.Categories.Commands.Internal.Edit;

public sealed record EditCategoryCommand(
	CategoryId Id,
	CategoryWriteDto Dto
) : ICommand;
