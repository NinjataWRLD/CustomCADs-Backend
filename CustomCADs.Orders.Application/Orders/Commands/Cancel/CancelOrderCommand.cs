using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Application.Orders.Commands.Cancel;

public record CancelOrderCommand(
    OrderId Id,
    UserId CancellerId
) : ICommand;
