using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Printing.Endpoints.Materials.Endpoints.Put.PresignedUrl;

public sealed record GetMaterialPutPresignedUrlRequest(
	int Id,
	UploadFileRequest File
);
