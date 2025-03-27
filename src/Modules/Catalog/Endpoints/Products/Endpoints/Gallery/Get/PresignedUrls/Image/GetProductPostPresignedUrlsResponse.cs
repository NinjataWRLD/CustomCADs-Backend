namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Gallery.Get.PresignedUrls.Image;

public sealed record GetProductGetPresignedUrlsResponse(
    string PresignedUrl,
    string ContentType
);
