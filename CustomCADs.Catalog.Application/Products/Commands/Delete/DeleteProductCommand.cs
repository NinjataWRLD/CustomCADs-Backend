namespace CustomCADs.Catalog.Application.Products.Commands.Delete;

public record DeleteProductCommand(ProductId Id) : ICommand;