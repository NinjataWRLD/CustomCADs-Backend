namespace CustomCADs.Catalog.Endpoints.Products.Gallery.Get.PresignedUrls.Image;

public sealed record GetProductGetPresignedUrlsResponse(
    string PresignedUrl,
    string ContentType
);
