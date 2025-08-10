using CustomCADs.Shared.Application.Dtos.Files;

namespace CustomCADs.Printing.Endpoints.Materials.Endpoints.Put.PresignedUrl;

public sealed record GetMaterialPutPresignedUrlRequest(
	int Id,
	UploadFileRequest File
);
