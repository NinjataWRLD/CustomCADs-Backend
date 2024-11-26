using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Orders.Application.Orders.Commands.Remove;

public record RemoveOrderCommand(
    OrderId Id,
    UserId RemoverId
) : ICommand;
