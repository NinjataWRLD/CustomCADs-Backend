using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Orders.Application.Orders.Commands.Accept;

public record AcceptOrderCommand(
    OrderId Id,
    AccountId DesignerId
) : ICommand;
