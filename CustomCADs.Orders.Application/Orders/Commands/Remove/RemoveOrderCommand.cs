using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Application.Orders.Commands.Remove;

public record RemoveOrderCommand(
    OrderId Id,
    UserId RemoverId
) : ICommand;
