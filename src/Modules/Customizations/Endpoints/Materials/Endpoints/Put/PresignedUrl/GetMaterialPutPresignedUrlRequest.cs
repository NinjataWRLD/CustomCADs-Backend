using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Customizations.Endpoints.Materials.Endpoints.Put.PresignedUrl;

public sealed record GetMaterialPutPresignedUrlRequest(
	int Id,
	UploadFileRequest File
);
