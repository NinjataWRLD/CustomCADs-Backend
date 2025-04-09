using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Post;

public sealed record CreatorGetProductCadPresignedUrlPostQuery(
    string ProductName,
    UploadFileRequest Cad
) : IQuery<UploadFileResponse>;
