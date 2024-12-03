using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.Orders.Commands.Report;

public sealed record ReportOrderCommand(
    OrderId Id,
    AccountId DesignerId
) : ICommand;
