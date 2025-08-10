namespace CustomCADs.Printing.Application.Customizations.Dtos;

public record CustomizationDto(
	CustomizationId Id,
	decimal Scale,
	decimal Infill,
	decimal Volume,
	decimal Weight,
	decimal Cost,
	string Color,
	MaterialId MaterialId
);
