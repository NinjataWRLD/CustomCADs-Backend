using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Designer.Post;

public sealed record GetCustomPostPresignedUrlRequest(
    Guid Id,
    UploadFileRequest Cad
);