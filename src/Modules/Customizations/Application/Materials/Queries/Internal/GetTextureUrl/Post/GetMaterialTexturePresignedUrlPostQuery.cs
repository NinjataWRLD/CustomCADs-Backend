namespace CustomCADs.Customizations.Application.Materials.Queries.Internal.GetTextureUrl.Post;

public sealed record GetMaterialTexturePresignedUrlPostQuery(
    string MaterialName,
    string ContentType,
    string FileName
) : IQuery<GetMaterialTexturePresignedUrlPostDto>;
