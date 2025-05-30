using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Get;
using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Get.PresignedUrls.Cad;

public sealed class GetProductGetPresignedCadUrlEndpoint(IRequestSender sender)
	: Endpoint<GetProductGetPresignedCadUrlRequest, DownloadFileResponse>
{
	public override void Configure()
	{
		Post("presignedUrls/download/cad");
		Group<CreatorGroup>();
		Description(d => d
			.WithSummary("Download Cad")
			.WithDescription("Download a Product's Cad")
		);
	}

	public override async Task HandleAsync(GetProductGetPresignedCadUrlRequest req, CancellationToken ct)
	{
		DownloadFileResponse response = await sender.SendQueryAsync(
			new CreatorGetProductCadPresignedUrlGetQuery(
				Id: ProductId.New(req.Id),
				CreatorId: User.GetAccountId()
			),
			ct
		).ConfigureAwait(false);

		await SendOkAsync(response).ConfigureAwait(false);
	}
}
