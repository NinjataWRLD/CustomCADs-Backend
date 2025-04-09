using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Put.PresignedUrl.Cad;

public sealed record GetProductPutCadPresignedUrlRequest(
    Guid Id,
    UploadFileRequest File
);
