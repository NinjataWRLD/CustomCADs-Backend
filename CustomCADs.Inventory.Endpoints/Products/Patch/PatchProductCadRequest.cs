﻿using CustomCADs.Inventory.Endpoints.Helpers.Dtos;
using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Inventory.Endpoints.Products.Patch;

public record PatchProductCadRequest(
    Guid Id,
    CoordinateType Type,
    CoordinatesDto Coordinates
);