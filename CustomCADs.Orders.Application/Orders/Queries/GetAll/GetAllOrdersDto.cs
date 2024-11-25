﻿using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Shared.Core.Common.TypedIds.Orders;

namespace CustomCADs.Orders.Application.Orders.Queries.GetAll;

public record GetAllOrdersDto(int Count, ICollection<GetAllOrdersItem> Orders);

public record GetAllOrdersItem(
    OrderId Id,
    string Name,
    DateTime OrderDate,
    DeliveryType DeliveryType,
    OrderStatus OrderStatus,
    string BuyerName,
    string? DesignerName
);