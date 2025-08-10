using CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.GetCadUrlGet;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Endpoints.Attributes;
using CustomCADs.Shared.Endpoints.Extensions;
using Microsoft.AspNetCore.Builder;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers.Get.PresignedCadUrl;

public sealed class GetCustomGetPresignedCadUrlEndpoint(IRequestSender sender)
	: Endpoint<GetCustomGetPresignedCadUrlRequest, DownloadFileResponse>
{
	public override void Configure()
	{
		Post("presignedUrls/download");
		Group<CustomerGroup>();
		Description(d => d
			.WithSummary("Download Cad")
			.WithDescription("Download the Cad for your Custom")
			.WithMetadata(new SkipIdempotencyAttribute())
		);
	}

	public override async Task HandleAsync(GetCustomGetPresignedCadUrlRequest req, CancellationToken ct)
	{
		DownloadFileResponse response = await sender.SendQueryAsync(
			new GetCustomCadPresignedUrlGetQuery(
				Id: CustomId.New(req.Id),
				BuyerId: User.GetAccountId()
			),
			ct
		).ConfigureAwait(false);

		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
