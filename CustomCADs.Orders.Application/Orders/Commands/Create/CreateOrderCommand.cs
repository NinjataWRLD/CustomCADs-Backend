using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Orders.Application.Orders.Commands.Create;

public record CreateOrderCommand(
    DeliveryType DeliveryType,
    string Name,
    string Description,
    UserId BuyerId
) : ICommand<OrderId>;
