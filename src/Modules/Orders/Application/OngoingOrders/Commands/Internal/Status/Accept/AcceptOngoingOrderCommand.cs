using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Internal.Status.Accept;

public sealed record AcceptOngoingOrderCommand(
    OngoingOrderId Id,
    AccountId DesignerId
) : ICommand;
