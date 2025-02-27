using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Status.Report;

public sealed record ReportOngoingOrderCommand(
    OngoingOrderId Id,
    AccountId DesignerId
) : ICommand;
