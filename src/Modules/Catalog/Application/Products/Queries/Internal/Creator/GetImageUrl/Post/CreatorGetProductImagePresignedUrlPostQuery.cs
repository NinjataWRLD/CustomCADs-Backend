using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Post;

public sealed record CreatorGetProductImagePresignedUrlPostQuery(
	string ProductName,
	UploadFileRequest Image
) : IQuery<UploadFileResponse>;
