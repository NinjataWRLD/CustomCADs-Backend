﻿using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Application.Orders.Commands.Create;

public record CreateOrderCommand(
    DeliveryType DeliveryType,
    string Name,
    string Description,
    string ImageKey,
    string ImageContentType,
    UserId BuyerId
) : ICommand<OrderId>;
