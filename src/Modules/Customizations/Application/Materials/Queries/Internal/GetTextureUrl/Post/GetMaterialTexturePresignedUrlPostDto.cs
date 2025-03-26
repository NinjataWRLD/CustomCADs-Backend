namespace CustomCADs.Customizations.Application.Materials.Queries.Internal.GetTextureUrl.Post;

public record GetMaterialTexturePresignedUrlPostDto(
    string GeneratedKey,
    string PresignedUrl
);
