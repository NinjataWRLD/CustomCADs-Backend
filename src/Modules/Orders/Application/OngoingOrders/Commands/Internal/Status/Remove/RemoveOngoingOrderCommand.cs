namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Internal.Status.Remove;

public sealed record RemoveOngoingOrderCommand(
    OngoingOrderId Id
) : ICommand;
