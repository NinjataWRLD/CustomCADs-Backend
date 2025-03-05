namespace CustomCADs.Customizations.Application.Materials.Queries.GetTextureUrl.Post;

public sealed record GetMaterialTexturePresignedUrlPostQuery(
    string MaterialName,
    string ContentType,
    string FileName
) : IQuery<GetMaterialTexturePresignedUrlPostDto>;
