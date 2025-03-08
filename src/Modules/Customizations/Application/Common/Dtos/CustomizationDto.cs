﻿namespace CustomCADs.Customizations.Application.Common.Dtos;

public record CustomizationDto(
    CustomizationId Id,
    decimal Scale,
    decimal Infill,
    decimal Volume,
    double Weight,
    decimal Cost,
    string Color,
    MaterialId MaterialId
);
