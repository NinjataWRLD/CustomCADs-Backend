using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.GetCadUrlGet;

public sealed record GetCustomCadPresignedUrlGetQuery(
	CustomId Id,
	AccountId BuyerId
) : IQuery<DownloadFileResponse>;
