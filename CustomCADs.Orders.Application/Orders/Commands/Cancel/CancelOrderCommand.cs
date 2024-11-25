using CustomCADs.Shared.Core.Common.TypedIds.Account;
using CustomCADs.Shared.Core.Common.TypedIds.Orders;

namespace CustomCADs.Orders.Application.Orders.Commands.Cancel;

public record CancelOrderCommand(
    OrderId Id,
    UserId CancellerId
) : ICommand;
