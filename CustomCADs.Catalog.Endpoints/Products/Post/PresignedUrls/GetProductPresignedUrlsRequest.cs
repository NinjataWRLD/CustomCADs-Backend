namespace CustomCADs.Catalog.Endpoints.Products.Post.PresignedUrls;

public record GetProductPresignedUrlsRequest(
    string ProductName,
    string ImageContentType,
    string ImageFileName,
    string CadContentType,
    string CadFileName
);
