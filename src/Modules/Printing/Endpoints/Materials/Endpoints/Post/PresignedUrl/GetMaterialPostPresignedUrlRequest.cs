using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Printing.Endpoints.Materials.Endpoints.Post.PresignedUrl;

public sealed record GetMaterialPostPresignedUrlRequest(
	string MaterialName,
	UploadFileRequest Image
);
