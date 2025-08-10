using CustomCADs.Shared.Application.Dtos.Files;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Designer.Post;

public sealed record GetCustomPostPresignedUrlRequest(
	Guid Id,
	UploadFileRequest Cad
);
