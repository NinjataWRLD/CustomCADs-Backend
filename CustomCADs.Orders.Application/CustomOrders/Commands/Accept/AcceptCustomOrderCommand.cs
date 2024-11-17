using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Application.CustomOrders.Commands.Accept;

public record AcceptCustomOrderCommand(
    CustomOrderId Id,
    UserId DesignerId
) : ICommand;
