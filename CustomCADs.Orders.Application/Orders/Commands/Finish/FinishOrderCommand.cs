using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Orders.Application.Orders.Commands.Finish;

public record FinishOrderCommand(
    OrderId Id,
    UserId FinisherId,
    (string Key, string ContentType)? Cad
) : ICommand;
