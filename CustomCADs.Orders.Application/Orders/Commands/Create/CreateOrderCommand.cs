using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Account;
using CustomCADs.Shared.Core.Common.TypedIds.Orders;

namespace CustomCADs.Orders.Application.Orders.Commands.Create;

public record CreateOrderCommand(
    DeliveryType DeliveryType,
    string Name,
    string Description,
    UserId BuyerId
) : ICommand<OrderId>;
