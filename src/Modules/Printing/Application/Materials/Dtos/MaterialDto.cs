namespace CustomCADs.Printing.Application.Materials.Dtos;

public record MaterialDto(
	MaterialId Id,
	string Name,
	decimal Density,
	decimal Cost
);
