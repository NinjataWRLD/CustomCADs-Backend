﻿namespace CustomCADs.Orders.Endpoints.Orders.Client.Post.Orders;

public sealed record PostOrderResponse(
    Guid Id,
    string Name,
    string Description,
    string OrderDate,
    string DeliveryType,
    string OrderStatus
);
