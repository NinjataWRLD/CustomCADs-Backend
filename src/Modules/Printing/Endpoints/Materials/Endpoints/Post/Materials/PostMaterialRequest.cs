namespace CustomCADs.Printing.Endpoints.Materials.Endpoints.Post.Materials;

public sealed record PostMaterialRequest(
	string Name,
	decimal Density,
	decimal Cost,
	string TextureKey,
	string TextureContentType
);
