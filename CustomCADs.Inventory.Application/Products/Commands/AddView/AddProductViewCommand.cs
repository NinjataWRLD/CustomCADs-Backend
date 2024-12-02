namespace CustomCADs.Inventory.Application.Products.Commands.AddView;

public record AddProductViewCommand(
    ProductId Id
) : ICommand;
