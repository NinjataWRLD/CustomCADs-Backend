namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Designer.Post;

public sealed record GetCustomPostPresignedUrlRequest(
    Guid Id,
    string ContentType,
    string FileName
);