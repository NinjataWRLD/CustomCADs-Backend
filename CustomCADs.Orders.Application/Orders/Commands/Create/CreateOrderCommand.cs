﻿using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.Orders.Commands.Create;

public sealed record CreateOrderCommand(
    DeliveryType DeliveryType,
    string Name,
    string Description,
    AccountId BuyerId
) : ICommand<OrderId>;
