namespace CustomCADs.Orders.Application.CustomOrders.Commands.Delete;

public record DeleteCustomOrderCommand(CustomOrderId Id) : ICommand;
