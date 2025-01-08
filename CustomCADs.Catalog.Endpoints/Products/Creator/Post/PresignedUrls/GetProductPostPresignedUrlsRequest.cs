namespace CustomCADs.Catalog.Endpoints.Products.Creator.Post.PresignedUrls;

public sealed record GetProductPostPresignedUrlsRequest(
    string ProductName,
    string ImageContentType,
    string ImageFileName,
    string CadContentType,
    string CadFileName
);
