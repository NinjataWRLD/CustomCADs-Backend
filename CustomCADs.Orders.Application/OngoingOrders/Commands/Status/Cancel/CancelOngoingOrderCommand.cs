using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Status.Cancel;

public sealed record CancelOngoingOrderCommand(
    OngoingOrderId Id,
    AccountId DesignerId
) : ICommand;
