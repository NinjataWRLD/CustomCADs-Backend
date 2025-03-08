namespace CustomCADs.Customizations.Application.Common.Dtos;

public record MaterialDto(
    MaterialId Id,
    string Name,
    decimal Density,
    decimal Cost
);
