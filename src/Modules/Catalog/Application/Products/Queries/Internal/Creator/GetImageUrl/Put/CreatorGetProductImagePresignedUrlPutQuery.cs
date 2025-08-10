using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Put;

public sealed record CreatorGetProductImagePresignedUrlPutQuery(
	ProductId Id,
	UploadFileRequest NewImage,
	AccountId CreatorId
) : IQuery<CreatorGetProductImagePresignedUrlPutDto>;
