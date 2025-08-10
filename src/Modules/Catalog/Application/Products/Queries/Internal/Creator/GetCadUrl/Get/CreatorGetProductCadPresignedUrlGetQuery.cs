using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Get;

public sealed record CreatorGetProductCadPresignedUrlGetQuery(
	ProductId Id,
	AccountId CreatorId
) : IQuery<DownloadFileResponse>;
