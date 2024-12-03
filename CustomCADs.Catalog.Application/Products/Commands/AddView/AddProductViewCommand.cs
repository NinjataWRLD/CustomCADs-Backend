namespace CustomCADs.Catalog.Application.Products.Commands.AddView;

public sealed record AddProductViewCommand(
    ProductId Id
) : ICommand;
