
namespace CustomCADs.Customizations.Application.Materials.Queries.GetTextureUrl.Put;

public sealed record GetMaterialTexturePresignedUrlPutQuery(
    MaterialId Id,
    string ContentType,
    string FileName
) : IQuery<GetMaterialTexturePresignedUrlPutDto>;
