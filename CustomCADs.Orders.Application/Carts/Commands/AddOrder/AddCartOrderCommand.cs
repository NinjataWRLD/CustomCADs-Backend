using CustomCADs.Orders.Domain.Common.Enums;
using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Catalog;

namespace CustomCADs.Orders.Application.Carts.Commands.AddOrder;

public record AddCartOrderCommand(
    CartId Id,
    DeliveryType DeliveryType,
    Money Price,
    int Quantity,
    ProductId ProductId
) : ICommand;
