using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Application.Orders.Commands.Report;

public record ReportOrderCommand(
    OrderId Id,
    UserId ReporterId
) : ICommand;
