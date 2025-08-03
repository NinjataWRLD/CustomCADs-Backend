using CustomCADs.Customizations.Application.Materials.Queries.Internal.GetTextureUrl.Get;
using CustomCADs.Shared.Core.Common.Dtos;
using Microsoft.AspNetCore.Builder;

namespace CustomCADs.Customizations.Endpoints.Materials.Endpoints.Get.PresignedUrl;

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
		var response = await sender.SendQueryAsync(
			new GetMaterialTexturePresignedUrlGetQuery(
				Id: MaterialId.New(req.Id)
			),
			ct
		).ConfigureAwait(false);


		await SendOkAsync(response).ConfigureAwait(false);
	}
}
