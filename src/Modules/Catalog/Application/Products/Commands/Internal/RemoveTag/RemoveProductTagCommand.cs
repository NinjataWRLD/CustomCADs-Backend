namespace CustomCADs.Catalog.Application.Products.Commands.Internal.RemoveTag;

public record RemoveProductTagCommand(
    ProductId Id,
    TagId TagId
) : ICommand;
