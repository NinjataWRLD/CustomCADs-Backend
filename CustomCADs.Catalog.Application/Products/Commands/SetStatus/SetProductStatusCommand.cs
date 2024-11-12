namespace CustomCADs.Catalog.Application.Products.Commands.SetStatus;

public record SetProductStatusCommand(ProductId Id, string Action) : ICommand;
