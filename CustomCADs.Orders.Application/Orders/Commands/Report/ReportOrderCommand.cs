using CustomCADs.Shared.Core.Common.TypedIds.Account;
using CustomCADs.Shared.Core.Common.TypedIds.Orders;

namespace CustomCADs.Orders.Application.Orders.Commands.Report;

public record ReportOrderCommand(
    OrderId Id,
    UserId ReporterId
) : ICommand;
