namespace CustomCADs.Customizations.Application.Materials.Queries.Internal.GetTextureUrl.Get;

public sealed record GetMaterialTexturePresignedUrlGetQuery(
    MaterialId Id
) : IQuery<GetMaterialTexturePresignedUrlGetDto>;
