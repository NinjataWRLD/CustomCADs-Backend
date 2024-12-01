using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.Orders.Commands.Accept;

public record AcceptOrderCommand(
    OrderId Id,
    AccountId DesignerId
) : ICommand;
