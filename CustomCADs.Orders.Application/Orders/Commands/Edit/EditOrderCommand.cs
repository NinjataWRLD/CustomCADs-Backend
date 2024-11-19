namespace CustomCADs.Orders.Application.Orders.Commands.Edit;

public record EditOrderCommand(
    OrderId Id,
    string Name,
    string Description
) : ICommand;
