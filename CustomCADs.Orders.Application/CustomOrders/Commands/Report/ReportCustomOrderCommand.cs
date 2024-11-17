using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Application.CustomOrders.Commands.Report;

public record ReportCustomOrderCommand(
    CustomOrderId Id,
    UserId ReporterId
) : ICommand;
