﻿namespace CustomCADs.Carts.Endpoints.Common.Dtos;

public sealed record PurchasedCartItemResponse(
    int Quantity,
    bool ForDelivery,
    decimal Price,
    Guid ProductId,
    Guid CartId,
    decimal Cost
);