using CustomCADs.Shared.Core.Common.TypedIds.Account;
using CustomCADs.Shared.Core.Common.TypedIds.Orders;

namespace CustomCADs.Orders.Application.Orders.Commands.Accept;

public record AcceptOrderCommand(
    OrderId Id,
    UserId DesignerId
) : ICommand;
