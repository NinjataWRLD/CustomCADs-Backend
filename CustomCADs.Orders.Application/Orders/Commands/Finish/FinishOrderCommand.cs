using CustomCADs.Shared.Core.Common.TypedIds.Account;
using CustomCADs.Shared.Core.Common.TypedIds.Orders;

namespace CustomCADs.Orders.Application.Orders.Commands.Finish;

public record FinishOrderCommand(
    OrderId Id,
    UserId FinisherId,
    (string Key, string ContentType)? Cad
) : ICommand;
