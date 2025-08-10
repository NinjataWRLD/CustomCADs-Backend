namespace CustomCADs.Printing.Endpoints.Customizations.Endpoints.Put;

public record EditCustomizationRequest(
	Guid Id,
	decimal Scale,
	decimal Infill,
	decimal Volume,
	string Color,
	int MaterialId
);
