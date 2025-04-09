using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Post.PresignedUrls;

public sealed record GetProductPostPresignedUrlsRequest(
    string ProductName,
    UploadFileRequest Image,
    UploadFileRequest Cad
);
