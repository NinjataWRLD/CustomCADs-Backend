using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.GetCadUrlGet;

public sealed record GetCustomCadPresignedUrlGetQuery(
	CustomId Id,
	AccountId BuyerId
) : IQuery<DownloadFileResponse>;
