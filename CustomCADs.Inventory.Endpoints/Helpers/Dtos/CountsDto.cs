﻿namespace CustomCADs.Inventory.Endpoints.Helpers.Dtos;

public sealed record CountsDto(
    int Purchases,
    int Likes,
    int Views
);
