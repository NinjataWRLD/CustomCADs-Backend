namespace CustomCADs.Customizations.Application.Materials.Queries.GetTextureUrl.Get;

public sealed record GetMaterialTexturePresignedUrlGetQuery(
    MaterialId Id
) : IQuery<GetMaterialTexturePresignedUrlGetDto>;
