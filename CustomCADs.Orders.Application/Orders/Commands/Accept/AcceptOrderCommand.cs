using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Application.Orders.Commands.Accept;

public record AcceptOrderCommand(
    OrderId Id,
    UserId DesignerId
) : ICommand;
