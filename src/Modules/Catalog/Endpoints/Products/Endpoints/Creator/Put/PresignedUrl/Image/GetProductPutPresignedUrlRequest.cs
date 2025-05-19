using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Put.PresignedUrl.Image;

public sealed record GetProductPutPresignedUrlRequest(
	Guid Id,
	UploadFileRequest File
);
