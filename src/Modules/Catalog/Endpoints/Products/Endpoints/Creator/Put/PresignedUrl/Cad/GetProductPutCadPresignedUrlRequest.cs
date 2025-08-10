using CustomCADs.Shared.Application.Dtos.Files;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Put.PresignedUrl.Cad;

public sealed record GetProductPutCadPresignedUrlRequest(
	Guid Id,
	UploadFileRequest File
);
