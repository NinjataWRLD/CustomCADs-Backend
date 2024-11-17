namespace CustomCADs.Orders.Application.Carts.Commands.Delete;

public record DeleteCartCommand(CartId Id) : ICommand;
