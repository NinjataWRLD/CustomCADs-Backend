﻿namespace CustomCADs.Gallery.Endpoints.Carts.Get.All;

public sealed record GetCartsDto(
    Guid Id,
    decimal Total,
    string PurchaseDate,
    int ItemsCount
);