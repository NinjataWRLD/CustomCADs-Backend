namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Status.Remove;

public sealed record RemoveOngoingOrderCommand(
    OngoingOrderId Id
) : ICommand;
