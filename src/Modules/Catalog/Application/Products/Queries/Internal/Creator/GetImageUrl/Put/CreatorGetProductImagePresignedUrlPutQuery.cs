using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Put;

public sealed record CreatorGetProductImagePresignedUrlPutQuery(
	ProductId Id,
	UploadFileRequest NewImage,
	AccountId CreatorId
) : IQuery<CreatorGetProductImagePresignedUrlPutDto>;
