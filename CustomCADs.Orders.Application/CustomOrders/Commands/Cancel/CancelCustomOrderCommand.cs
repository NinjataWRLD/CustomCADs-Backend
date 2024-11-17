using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Application.CustomOrders.Commands.Cancel;

public record CancelCustomOrderCommand(
    CustomOrderId Id,
    UserId CancellerId
) : ICommand;
