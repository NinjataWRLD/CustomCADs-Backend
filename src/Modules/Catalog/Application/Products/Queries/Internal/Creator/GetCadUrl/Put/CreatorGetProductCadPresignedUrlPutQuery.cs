using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Put;

public sealed record CreatorGetProductCadPresignedUrlPutQuery(
	ProductId Id,
	UploadFileRequest NewCad,
	AccountId CreatorId
) : IQuery<CreatorGetProductCadPresignedUrlPutDto>;
