using CustomCADs.Shared.Core.Common.TypedIds.Account;
using CustomCADs.Shared.Core.Common.TypedIds.Orders;

namespace CustomCADs.Orders.Application.Orders.Commands.Begin;

public record BeginOrderCommand(
    OrderId Id,
    UserId BeginnerId
) : ICommand;
