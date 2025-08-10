namespace CustomCADs.Printing.Endpoints.Customizations.Dtos;

public record CustomizationResponse(
	Guid Id,
	decimal Scale,
	decimal Infill,
	decimal Weight,
	decimal Cost,
	string Color,
	int MaterialId
);
