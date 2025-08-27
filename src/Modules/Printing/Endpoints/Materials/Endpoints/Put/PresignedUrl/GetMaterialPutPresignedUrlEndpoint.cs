using CustomCADs.Printing.Application.Materials.Queries.Internal.GetTextureUrl.Put;
using CustomCADs.Shared.Endpoints.Attributes;
using Microsoft.AspNetCore.Builder;

namespace CustomCADs.Printing.Endpoints.Materials.Endpoints.Put.PresignedUrl;

public sealed class GetMaterialPutPresignedUrlEndpoint(IRequestSender sender)
	: Endpoint<GetMaterialPutPresignedUrlRequest, GetMaterialTexturePresignedUrlPutDto>
{
	public override void Configure()
	{
		Post("presignedUrls/replace");
		Group<MaterialsGroup>();
		Description(d => d
			.WithSummary("Change Texture")
			.WithDescription("Change your Material's Texture")
			.WithMetadata(new SkipIdempotencyAttribute())
		);
	}

	public override async Task HandleAsync(GetMaterialPutPresignedUrlRequest req, CancellationToken ct)
	{
		GetMaterialTexturePresignedUrlPutDto response = await sender.SendQueryAsync(
			new GetMaterialTexturePresignedUrlPutQuery(
				Id: MaterialId.New(req.Id),
				NewImage: req.File
			),
			ct
		).ConfigureAwait(false);

		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
