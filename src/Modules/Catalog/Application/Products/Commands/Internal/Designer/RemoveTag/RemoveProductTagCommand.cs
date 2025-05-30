namespace CustomCADs.Catalog.Application.Products.Commands.Internal.Designer.RemoveTag;

public record RemoveProductTagCommand(
	ProductId Id,
	TagId TagId
) : ICommand;
