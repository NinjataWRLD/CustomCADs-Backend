using CustomCADs.Printing.Application.Materials.Queries.Internal.GetTextureUrl.Get;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Endpoints.Attributes;
using Microsoft.AspNetCore.Builder;

namespace CustomCADs.Printing.Endpoints.Materials.Endpoints.Get.PresignedUrl;

public sealed class GetMaterialGetPresignedUrlEndpoint(IRequestSender sender)
	: Endpoint<GetMaterialGetPresignedUrlRequest, DownloadFileResponse>
{
	public override void Configure()
	{
		Post("presignedUrls/download");
		Group<MaterialsGroup>();
		AllowAnonymous();
		Description(d => d
			.WithSummary("Download Texture")
			.WithDescription("Download your Material's Texture")
			.WithMetadata(new SkipIdempotencyAttribute())
		);
	}

	public override async Task HandleAsync(GetMaterialGetPresignedUrlRequest req, CancellationToken ct)
	{
		DownloadFileResponse response = await sender.SendQueryAsync(
			new GetMaterialTexturePresignedUrlGetQuery(
				Id: MaterialId.New(req.Id)
			),
			ct
		).ConfigureAwait(false);

		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
