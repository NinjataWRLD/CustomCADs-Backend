using CustomCADs.Shared.Application.Dtos.Files;

namespace CustomCADs.Printing.Endpoints.Materials.Endpoints.Post.PresignedUrl;

public sealed record GetMaterialPostPresignedUrlRequest(
	string MaterialName,
	UploadFileRequest Image
);
