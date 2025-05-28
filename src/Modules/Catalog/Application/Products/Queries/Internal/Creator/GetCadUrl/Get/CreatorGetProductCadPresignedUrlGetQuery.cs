using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Get;

public sealed record CreatorGetProductCadPresignedUrlGetQuery(
	ProductId Id,
	AccountId CreatorId
) : IQuery<DownloadFileResponse>;
