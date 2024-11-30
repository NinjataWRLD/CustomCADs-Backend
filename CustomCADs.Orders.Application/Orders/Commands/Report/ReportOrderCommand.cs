using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Orders.Application.Orders.Commands.Report;

public record ReportOrderCommand(
    OrderId Id,
    AccountId ReporterId
) : ICommand;
