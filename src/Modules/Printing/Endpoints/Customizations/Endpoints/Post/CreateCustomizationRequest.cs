namespace CustomCADs.Printing.Endpoints.Customizations.Endpoints.Post;

public record CreateCustomizationRequest(
	decimal Scale,
	decimal Infill,
	decimal Volume,
	string Color,
	int MaterialId
);
