using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.Orders.Commands.Report;

public record ReportOrderCommand(
    OrderId Id,
    AccountId DesignerId
) : ICommand;
