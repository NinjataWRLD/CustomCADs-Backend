namespace CustomCADs.Catalog.Application.Products.Commands.Internal.Designer.AddTag;

public record AddProductTagCommand(
	ProductId Id,
	TagId TagId
) : ICommand;
