using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Customizations.Endpoints.Materials.Endpoints.Post.PresignedUrl;

public sealed record GetMaterialPostPresignedUrlRequest(
	string MaterialName,
	UploadFileRequest Image
);
