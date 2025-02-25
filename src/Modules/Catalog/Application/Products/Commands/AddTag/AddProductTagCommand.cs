namespace CustomCADs.Catalog.Application.Products.Commands.AddTag;

public record AddProductTagCommand(
    ProductId Id,
    TagId TagId
) : ICommand;
