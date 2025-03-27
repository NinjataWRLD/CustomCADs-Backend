namespace CustomCADs.Catalog.Application.Products.Commands.Internal.AddTag;

public record AddProductTagCommand(
    ProductId Id,
    TagId TagId
) : ICommand;
