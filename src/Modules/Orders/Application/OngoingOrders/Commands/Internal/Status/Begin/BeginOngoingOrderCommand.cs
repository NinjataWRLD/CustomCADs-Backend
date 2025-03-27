using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Internal.Status.Begin;

public sealed record BeginOngoingOrderCommand(
    OngoingOrderId Id,
    AccountId DesignerId
) : ICommand;
