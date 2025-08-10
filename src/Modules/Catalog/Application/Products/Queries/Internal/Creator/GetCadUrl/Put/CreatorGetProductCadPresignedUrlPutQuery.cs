using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Put;

public sealed record CreatorGetProductCadPresignedUrlPutQuery(
	ProductId Id,
	UploadFileRequest NewCad,
	AccountId CreatorId
) : IQuery<CreatorGetProductCadPresignedUrlPutDto>;
