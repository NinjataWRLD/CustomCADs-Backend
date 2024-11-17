using CustomCADs.Orders.Domain.Common.Enums;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Application.Carts.Commands.Create;

public record CreateCartCommand(
    UserId BuyerId
) : ICommand<CartId>;
