﻿namespace CustomCADs.Inventory.Endpoints.Products.Put;

public record PutProductRequest(
    Guid Id,
    string Name,
    string Description,
    int CategoryId,
    decimal Price,
    string? ImageKey
);