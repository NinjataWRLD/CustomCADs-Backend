namespace CustomCADs.Printing.Endpoints.Materials.Dtos;

public sealed record MaterialResponse(
	int Id,
	string Name,
	decimal Density,
	decimal Cost
);
