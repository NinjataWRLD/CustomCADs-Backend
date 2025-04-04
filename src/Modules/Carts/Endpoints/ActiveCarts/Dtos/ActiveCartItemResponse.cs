﻿namespace CustomCADs.Carts.Endpoints.ActiveCarts.Dtos;

public sealed record ActiveCartItemResponse(
    int Quantity,
    bool ForDelivery,
    string AddedAt,
    Guid ProductId,
    Guid? CustomizationId
);