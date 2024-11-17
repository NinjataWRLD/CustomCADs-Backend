namespace CustomCADs.Orders.Application.CustomOrders.Commands.Edit;

public record EditCustomOrderCommand(
    CustomOrderId Id,
    string Name,
    string Description
) : ICommand;
