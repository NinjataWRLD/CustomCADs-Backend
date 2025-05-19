namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Gallery.Get.PresignedUrls.Cad;

public sealed record GetProductGetPresignedUrlsResponse(
	string PresignedUrl,
	string ContentType
);
