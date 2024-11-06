namespace CustomCADs.Catalog.Application.Products.Commands.SetStatus;

public record SetProductStatusCommand(Guid Id, string Action) : ICommand;
