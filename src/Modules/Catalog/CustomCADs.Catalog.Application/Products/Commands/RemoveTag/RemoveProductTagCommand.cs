namespace CustomCADs.Catalog.Application.Products.Commands.RemoveTag;

public record RemoveProductTagCommand(
    ProductId Id,
    TagId TagId
) : ICommand;
