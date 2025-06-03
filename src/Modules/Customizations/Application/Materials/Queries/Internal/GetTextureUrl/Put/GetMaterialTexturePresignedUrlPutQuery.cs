using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Customizations.Application.Materials.Queries.Internal.GetTextureUrl.Put;

public sealed record GetMaterialTexturePresignedUrlPutQuery(
	MaterialId Id,
	UploadFileRequest NewImage
) : IQuery<GetMaterialTexturePresignedUrlPutDto>;
