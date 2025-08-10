using CustomCADs.Shared.Application.Dtos.Files;

namespace CustomCADs.Printing.Application.Materials.Queries.Internal.GetTextureUrl.Put;

public sealed record GetMaterialTexturePresignedUrlPutQuery(
	MaterialId Id,
	UploadFileRequest NewImage
) : IQuery<GetMaterialTexturePresignedUrlPutDto>;
