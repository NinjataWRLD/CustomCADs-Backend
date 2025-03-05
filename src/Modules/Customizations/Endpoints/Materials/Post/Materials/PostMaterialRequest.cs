namespace CustomCADs.Customizations.Endpoints.Materials.Post.Materials;

public sealed record PostMaterialRequest(
    string Name,
    decimal Density,
    decimal Cost,
    string TextureKey,
    string TextureContentType
);
