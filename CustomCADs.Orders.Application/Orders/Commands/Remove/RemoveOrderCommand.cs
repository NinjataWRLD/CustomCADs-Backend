using CustomCADs.Shared.Core.Common.TypedIds.Account;
using CustomCADs.Shared.Core.Common.TypedIds.Orders;

namespace CustomCADs.Orders.Application.Orders.Commands.Remove;

public record RemoveOrderCommand(
    OrderId Id,
    UserId RemoverId
) : ICommand;
