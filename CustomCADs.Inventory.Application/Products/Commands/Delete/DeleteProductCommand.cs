namespace CustomCADs.Inventory.Application.Products.Commands.Delete;

public record DeleteProductCommand(ProductId Id) : ICommand;