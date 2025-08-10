namespace CustomCADs.Printing.Endpoints.Materials.Endpoints.Put.Materials;

public sealed record PutMaterialRequest(
	int Id,
	string Name,
	decimal Density,
	decimal Cost,
	string? TextureKey,
	string? TextureContentType
);
