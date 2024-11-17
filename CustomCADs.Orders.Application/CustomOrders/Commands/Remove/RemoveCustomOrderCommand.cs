using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Application.CustomOrders.Commands.Remove;

public record RemoveCustomOrderCommand(
    CustomOrderId Id,
    UserId RemoverId
) : ICommand;
