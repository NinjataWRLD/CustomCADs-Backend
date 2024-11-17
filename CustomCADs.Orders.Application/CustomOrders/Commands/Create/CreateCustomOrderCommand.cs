using CustomCADs.Orders.Domain.Common.Enums;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Application.CustomOrders.Commands.Create;

public record CreateCustomOrderCommand(
    DeliveryType DeliveryType,
    string Name,
    string Description,
    UserId BuyerId
) : ICommand<CustomOrderId>;
