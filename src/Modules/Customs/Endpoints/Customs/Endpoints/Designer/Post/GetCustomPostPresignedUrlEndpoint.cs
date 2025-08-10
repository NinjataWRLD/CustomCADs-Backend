using CustomCADs.Customs.Application.Customs.Queries.Internal.Designer.GetCadUrlPost;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Endpoints.Attributes;
using CustomCADs.Shared.Endpoints.Extensions;
using Microsoft.AspNetCore.Builder;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Designer.Post;

public sealed class GetCustomPostPresignedUrlEndpoint(IRequestSender sender)
	: Endpoint<GetCustomPostPresignedUrlRequest, UploadFileResponse>
{
	public override void Configure()
	{
		Post("presignedUrls/upload/cad");
		Group<DesignerGroup>();
		Description(d => d
			.WithSummary("Upload")
			.WithDescription("Upload a Cad for an Custom")
			.WithMetadata(new SkipIdempotencyAttribute())
		);
	}

	public override async Task HandleAsync(GetCustomPostPresignedUrlRequest req, CancellationToken ct)
	{
		UploadFileResponse response = await sender.SendQueryAsync(
			new GetCustomCadPresignedUrlPostQuery(
				Id: CustomId.New(req.Id),
				Cad: req.Cad,
				DesignerId: User.GetAccountId()
			),
			ct
		).ConfigureAwait(false);

		await SendOkAsync(response).ConfigureAwait(false);
	}
}
