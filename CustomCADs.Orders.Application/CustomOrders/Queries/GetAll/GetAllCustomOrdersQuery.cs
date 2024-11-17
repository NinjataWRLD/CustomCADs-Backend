﻿using CustomCADs.Orders.Domain.Common.Enums;
using CustomCADs.Orders.Domain.CustomOrders.Enums;
using CustomCADs.Orders.Domain.CustomOrders.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Application.CustomOrders.Queries.GetAll;

public record GetAllCustomOrdersQuery(
    DeliveryType? DeliveryType = null,
    CustomOrderStatus? OrderStatus = null,
    UserId? BuyerId = null,
    UserId? DesignerId = null,
    string? Name = null,
    CustomOrderSorting? Sorting = null,
    int Page = 1,
    int Limit = 20
) : IQuery<GetAllCustomOrdersDto>;
