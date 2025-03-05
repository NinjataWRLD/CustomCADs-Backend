namespace CustomCADs.Customizations.Application.Materials.Queries.GetTextureUrl.Post;

public record GetMaterialTexturePresignedUrlPostDto(
    string GeneratedKey,
    string PresignedUrl
);
